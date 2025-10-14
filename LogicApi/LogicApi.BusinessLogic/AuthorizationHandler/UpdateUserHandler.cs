
using Common.Blob;
using Common.Utils.NamesHelper;
using LogicApi.Model.Request.Authorization;
using LogicCommon.Model.Request.File;
using PersistenceDb.Models.Administration;
using PersistenceDb.Models.Authentication;
namespace LogicApi.BusinessLogic.AuthorizationHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateUserHandler(
    ILogger<UpdateUserHandler> logger,
    IPluginFactory pluginFactory) : AuthorizationBase<UpdateUserRequest, HandlerResponse>(
        logger,
        pluginFactory)
{


    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<HandlerResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    => await ExecuteHandlerAsync(OperationApiName.UpdateUser, request, async () =>
        {
            //Obtiene las configuraciones Iniciales
            var currentUser = await GetUserFromContextAsync().ConfigureAwait(false);
            if (request.Image is not null)
            {
                if (currentUser.Image is not null)
                    await DeleteFileIfExist(currentUser, cancellationToken).ConfigureAwait(false);
                currentUser.Image = await SaveAndGetFile(request, currentUser, cancellationToken).ConfigureAwait(false);
            }
            currentUser.Phone = request.Phone;
            await AuthenticationUnitOfWork.UserRepository.UpdateAsync(currentUser).ConfigureAwait(false);
            //Consigue el Header del Switch
            return HandlerResponse.Complete(GetSuccessMessage(MessagesCodesSucess.UpdateUserInformationSuccess), true);
        }, UnitOfWorkType.Authentication);


    /// <summary>
    /// Almacena la imagen y la obtiene
    /// </summary>
    /// <param name="request"></param>
    /// <param name="currentUser"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<FilePersistence> SaveAndGetFile(UpdateUserRequest request, User currentUser, CancellationToken cancellationToken)
    {
        var fileName = BlobNameHelper.GetUserImageName($"{currentUser?.Id}", request?.Image?.FileExtension);
        var path = ContainerNames.USER_IMAGE;
        var response = await Mediator.Send(new UpdateBlobFileRequest(
            request!.Image.EncodeContent,
             fileName,
              path,
              ContextRequest), cancellationToken).ConfigureAwait(false);
        var file = new FilePersistence
        {
            DateRegister = Clock.Now(),
            Name = response.FileName,
            Path = path,
            State = true,
            Url = response.Url
        };
        return file;
    }

    /// <summary>
    /// Elimina la imagen si existe
    /// </summary>
    /// <param name="currentUser"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task DeleteFileIfExist(User currentUser, CancellationToken cancellationToken)
    {
        _ = await Mediator.Send(new DeleteBlobFileRequest(
            currentUser.Image?.Name,
            ContainerNames.USER_IMAGE,
            ContextRequest), cancellationToken).ConfigureAwait(false);
        currentUser.Image.State = false;
        await AdministrationUnitOfWork.FileRepository.UpdateAsync(currentUser.Image).ConfigureAwait(false);
    }
}
