using Common.Blob.Models.Request;
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
                    ImageGuid = (Guid?)select.Image.Guid,
                    select.Phone
                },
                where => where.Id == UserId
            ).ConfigureAwait(false)
            ?? throw new CustomException((int)MessagesCodesError.InfoUserNotFound, $"El perfil del usuario: {UserId} no se encuentra en la base de datos");
            if (request.Image is not null)
            {
                List<RequestEncodeFile> images = [];
                if (currentUser.ImageGuid.HasValue)
                    images.Add(RequestEncodeFile.ToDelete(currentUser.ImageGuid.Value));
                images.Add(request.Image);
                await ProcessUserImageFiles(images, UserId).ConfigureAwait(false);
            }
            if (currentUser.Phone != request.Phone)
                await UnitOfWork.UserRepository.UpdateByAsync((user => user.Phone, request.Phone), where => where.Id == UserId).ConfigureAwait(false);
            //Consigue el Header del Switch
            return HandlerResponse.Complete(GetSuccessMessage(MessagesCodesSucess.UpdateUserInformationSuccess), true);
        });

    /// <summary>
    /// Procesa las imágenes de la sucursal
    /// </summary>
    /// <param name="images"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    protected async Task ProcessUserImageFiles(List<RequestEncodeFile> images, int userId)
      => await ProcessImagesAsync(
            images,
            PathCode.UserImage,
            processCreateImagesAsync: async (images, response) =>
            {
                var imageId = response.Items.FirstOrDefault()?.Id;
                await UnitOfWork.UserRepository.UpdateByAsync(
                    (user => user.ImagenId, imageId),
                    where => where.Id == userId).ConfigureAwait(false);
            },
            getFileExtension: (fileExtension) => HelperFileName.GetUserImageName(fileExtension)
        ).ConfigureAwait(false);


}
