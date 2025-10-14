using LogicApi.Model.Common;
using LogicApi.Model.Request.Authorization;
using LogicApi.Model.Response.Authorization;
namespace LogicApi.BusinessLogic.AuthorizationHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class RegisterBiometricHandler(
    ILogger<RegisterBiometricHandler> logger,
    IPluginFactory pluginFactory) : AuthorizationBase<RegisterBiometricRequest, RegisterBiometricResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<RegisterBiometricResponse> Handle(RegisterBiometricRequest request, CancellationToken cancellationToken)
    => await ExecuteHandlerAsync(OperationApiName.RegisterBiometric, request, async () =>
        {
            //Buscamos el registro relacionado al usuario y dispositivo
            var userDevice = await AuthenticationUnitOfWork.UserDeviceRepository
                .GetByFirstOrDefaultAsync(where => where.UserId == UserId && where.Device.MobileId == MobileId).ConfigureAwait(false)
                ?? throw new CustomException((int)MessagesCodesError.InformationNotFoundInDataBase, $"No se pudo encontrar el registro con Id de usuario: '{UserId}' y Id de Dipositivo: '{DeviceId}'");
            var biometric = new BiometricModel
            {
                Guid = Guid.NewGuid(),
                DateTimeGenerated = Now,
                PersonId = PersonId,
                UserId = UserId,
                DateTimeExpired = Now.AddDays(30),
            };
            var biometricEncrypted = await EncryptedValueAsync(biometric.ToJson()).ConfigureAwait(false);
            //Asigna el biométrico generado al usuario
            userDevice.Biometric = biometricEncrypted.ToSha256();
            //Actualiza el registro de biométrico
            _ = await AuthenticationUnitOfWork.UserDeviceRepository.UpdateAsync(userDevice).ConfigureAwait(false);
            //Retorna la respuesta 
            return new RegisterBiometricResponse
            {
                Biometric = biometricEncrypted,
                UserMessage = GetSuccessMessage(),
            };
        }, UnitOfWorkType.Authentication);
}
