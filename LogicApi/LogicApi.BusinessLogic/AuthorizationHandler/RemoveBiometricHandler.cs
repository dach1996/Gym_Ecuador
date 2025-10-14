using LogicApi.Model.Request.Authorization;
using PersistenceDb.Models.Authentication;
namespace LogicApi.BusinessLogic.AuthorizationHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class RemoveBiometricHandler(
    ILogger<RemoveBiometricHandler> logger,
    IPluginFactory pluginFactory) : AuthorizationBase<RemoveBiometricRequest, HandlerResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<HandlerResponse> Handle(RemoveBiometricRequest request, CancellationToken cancellationToken)
    => await ExecuteHandlerAsync(OperationApiName.RemoveBiometric, request, async () =>
        {
            //Actualiza el registro de biométrico
            var updateResult = await UnitOfWork.UserDeviceRepository.UpdateByAsync(
                update => new UserDevice
                {
                    Biometric = null,
                    RegisterDate = Now,
                },
                where => where.UserId == UserId && where.DeviceId == DeviceId
            ).ConfigureAwait(false);
            if (updateResult == 0)
                throw new CustomException((int)MessagesCodesError.InformationNotFoundInDataBase, $"No se pudo actualizar el registro de biométrico para el usuario: '{UserId}' y el dispositivo: '{DeviceId}'");
            //Retorna la respuesta 
            return HandlerResponse.Complete();
        });
}
