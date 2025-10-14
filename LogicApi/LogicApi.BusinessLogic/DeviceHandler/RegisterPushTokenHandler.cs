using LogicApi.Model.Request.Device;
using PersistenceDb.Models.Authentication;
namespace LogicApi.BusinessLogic.DeviceHandler;

/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class RegisterPushTokenHandler(
    ILogger<RegisterPushTokenHandler> logger,
    IPluginFactory pluginFactory) : DeviceBase<RegisterPushTokenRequest, HandlerResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async override Task<HandlerResponse> Handle(RegisterPushTokenRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.RegisterPushToken, request, async () =>
        {
            var deviceId = await AdministratorCache.TryGetAsync<int?>(CacheCodes.DeviceIdByMobileId(MobileId)).ConfigureAwait(false);
            if (!deviceId.HasValue)
            {
                deviceId = await UnitOfWork.DeviceRepository.GetFirstOrDefaultGenericAsync(
                                    select => select.Id,
                                    where => where.MobileId == MobileId
                                ).ConfigureAwait(false);
                await AdministratorCache.SetAsync(CacheCodes.DeviceIdByMobileId(MobileId), deviceId.Value, slidingExpiration: true).ConfigureAwait(false);
            }
            //Obtiene la información del token
            var userDevicePushTokenInformation = await UnitOfWork.UserDevicePushTokenRepository.GetFirstOrDefaultGenericAsync(
                select => new
                {
                    select.UserId,
                    select.PushToken
                },
                where => where.DeviceId == deviceId.Value
            ).ConfigureAwait(false);
            //Si no existe el registro, se crea
            if (userDevicePushTokenInformation is null)
            {
                var newUserDevicePushToken = await UnitOfWork.UserDevicePushTokenRepository.AddAsync(
                    new UserDevicePushToken
                    {
                        UserId = UserId,
                        DeviceId = deviceId.Value,
                        LastDateUpdated = Now,
                        NotificationPushImplementationType = ContextRequest.Headers.HasGoogleServices
                            ? NotificationPushImplementationType.Firebase
                            : NotificationPushImplementationType.Huawei,
                        PushToken = request.Token
                    },
                    autoDetectChangesEnabled: false
                ).ConfigureAwait(false);
                userDevicePushTokenInformation = new
                {
                    newUserDevicePushToken.UserId,
                    newUserDevicePushToken.PushToken
                };
            }
            //Si el token es diferente, se actualiza
            if (userDevicePushTokenInformation.PushToken != request.Token || userDevicePushTokenInformation.UserId != UserId)
                //Actualiza el token al usuario
                await UnitOfWork.UserDevicePushTokenRepository.UpdateByAsync(
                    update => new UserDevicePushToken
                    {
                        PushToken = request.Token,
                        LastDateUpdated = Now,
                        UserId = UserId
                    },
                    where => where.DeviceId == deviceId.Value,
                    autoDetectChangesEnabled: false
                ).ConfigureAwait(false);
            return HandlerResponse.Complete();
        });
}