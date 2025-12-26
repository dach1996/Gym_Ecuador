using Common.Utils.ImageTools;
using Common.WebCommon.Helper;
using Common.WebCommon.Models.Enum;
using LogicApi.Model.Request.Authorization;
using LogicCommon.Model.Request.File;
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
            var currentUser = await UnitOfWork.UserRepository.GetFirstOrDefaultGenericAsync(
                select => new
                {
                    select.Id,
                    ImageData = select.ImagenId.HasValue ? new
                    {
                        select.ImagenId,
                        select.Image.Name,
                        select.Image.Path,
                    } : null,
                    select.Phone
                },
                where => where.Id == UserId
            ).ConfigureAwait(false)
            ?? throw new CustomException((int)MessagesCodesError.InfoUserNotFound, $"El perfil del usuario: {UserId} no se encuentra en la base de datos");
            if (request.Image is not null)
            {
                if (currentUser.ImageData?.ImagenId.HasValue ?? false)
                    await DeleteFileIfExist(currentUser.ImageData.ImagenId.Value, currentUser.ImageData.Name, cancellationToken).ConfigureAwait(false);
                var updateFileItemResponse = (await Mediator.Send(new UpdateBlobFileRequest(
                    request.Image.EncodeContent,
                     HelperFileName.GetUserImageName(request.Image.FileExtension),
                      PathCode.UserImage,
                      ContextRequest
                      ), cancellationToken).ConfigureAwait(false)).Items.FirstOrDefault();
                if (updateFileItemResponse is not null)
                    await UnitOfWork.UserRepository.UpdateByAsync(
                        (user => user.ImagenId, updateFileItemResponse.Id),
                        where => where.Id == UserId).ConfigureAwait(false);
            }
            if (currentUser.Phone != request.Phone)
                await UnitOfWork.UserRepository.UpdateByAsync((user => user.Phone, request.Phone), where => where.Id == UserId).ConfigureAwait(false);
            //Consigue el Header del Switch
            return HandlerResponse.Complete(GetSuccessMessage(MessagesCodesSucess.UpdateUserInformationSuccess), true);
        });

    /// <summary>
    /// Elimina la imagen si existe
    /// </summary>
    /// <param name="currentUser"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task DeleteFileIfExist(int imageId, string imageName, CancellationToken cancellationToken)
    {
        var fileBasePaths = await GetFileBasePathCacheInformationByPathCodeAsync(PathCode.UserImage).ConfigureAwait(false);
        _ = await Mediator.Send(new DeleteBlobFileRequest(
            imageName,
            fileBasePaths.FileDirectoryPath,
            fileBasePaths.Implementation,
            ContextRequest), cancellationToken).ConfigureAwait(false);
        await UnitOfWork.FileRepository.UpdateByAsync((file => file.State, false), where => where.Id == imageId).ConfigureAwait(false);
    }
}
