using Common.PushNotification.Model;
using Common.Utils.CustomExceptions;
using Common.Utils.Extensions;
using Common.WebCommon.Models.Enum;
using LogicCommon.Model.Common;
using LogicCommon.Model.Request.NotificationPush;
using LogicCommon.Model.Response.NotificationPush;
using PersistenceDb.Models.Administration;
using PersistenceDb.Models.Enums;
using static Common.PushNotification.Model.NotificationTokens;

namespace LogicCommon.BusinessLogic.NotificationHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class SendNotificationPushUsersHandler(
    ILogger<SendNotificationPushUsersHandler> logger,
    IPluginFactory pluginFactory) : NotificationPushHandlerBase<SendNotificationPushUsersRequest, NotificationPushResponse>(logger, pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<NotificationPushResponse> Handle(SendNotificationPushUsersRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(request, async () =>
        {
            //Creamos la notificación
            var notificationPushRespository = await UnitOfWork.NotificationPushRepository.AddAsync(new()
            {
                Title = request.Title,
                Description = request.Body,
                RegisterDate = Clock.Now(),
                PushNotificationType = EnumsDataBase.PushNotificationType.Token,
                PushNotificationValue = $"{TopicType.Token}",
            }).ConfigureAwait(false);
            //Obtiene la lista de notificaciones push para enviar al usuario
            var usersTokenNotification = await GetUserTokenNotificationAsync(request).ConfigureAwait(false);
            //Crea el modelo para envío de Notificaciones
            var notificationToken = new NotificationTokens(
                    request.Title,
                    request.Body,
                    usersTokenNotification.Select(select => new NotificationTokenImplementationRequestModel
                    {
                        Token = select.Token,
                        Implementation = select.Implementation
                    }));
            //Envía Notificaciones
            var notificationPushResponse = await PushNotificationPlatform.SendNotificationAsync(notificationToken).ConfigureAwait(false);
            var notificationsPushDeviceRepository = usersTokenNotification.Join(
                notificationPushResponse.NotificationItems,
                usersTokenNotification => usersTokenNotification.Token,
                notificationPushResponse => notificationPushResponse.Identifier,
                (usersTokenNotification, notificationPushResponse) => new
                {
                    usersTokenNotification.UserId,
                    usersTokenNotification.DeviceId,
                    PushNotificationId = notificationPushRespository.Id,
                    AdditionalInformation = notificationPushResponse.IsSuccess ? notificationPushResponse.MessageId : notificationPushResponse.Message,
                    StatusNotification = notificationPushResponse.IsSuccess ? EnumsDataBase.StatusNotification.Sended : EnumsDataBase.StatusNotification.Error
                })
                .GroupBy(group => group.UserId)
                .Select(select => new NotificationPushUser
                {
                    UserId = select.Key,
                    PushNotificationId = notificationPushRespository.Id,
                    NotificationPushUserDevices = [.. select.Select(select => new NotificationPushUserDevice
                    {
                        DeviceId = select.DeviceId,
                        NotificationPushUserId = notificationPushRespository.Id,
                        StatusNotification = select.StatusNotification,
                        AdditionalInformation = select.AdditionalInformation
                    })]
                });
            //Almacena las notificaciones Push del usuario
            await UnitOfWork.NotificationPushUserRepository.AddRangeAsync(notificationsPushDeviceRepository, includeChildren: true).ConfigureAwait(false);
            return new NotificationPushResponse(
                notificationsPushDeviceRepository.ToDictionary(
                    key => key.UserId,
                    value => new NotificationPushResponse.NotificationPushUserResponse(
                        value.NotificationPushUserDevices.Count,
                        value.NotificationPushUserDevices.Count(countWhere => countWhere.StatusNotification == EnumsDataBase.StatusNotification.Sended)
                    )
                )
            );
        }).ConfigureAwait(false);

    /// <summary>
    /// Obtiene una lista de Notificaciones Push de Usuario
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private async Task<List<UserTokenNotification>> GetUserTokenNotificationAsync(SendNotificationPushUsersRequest request)
    {
        //Obtiene la información del los usuarios y dispositivos
        var usersTokenNotification = await UnitOfWork.UserDevicePushTokenRepository
            .GetGenericAsync(
                select => new UserTokenNotification
                {
                    DeviceId = select.DeviceId,
                    UserId = select.UserId.Value,
                    Token = select.PushToken,
                    Implementation = GetImplementation(select.NotificationPushImplementationType)
                },
                where => request.UsersId.Contains(where.UserId.Value) && !string.IsNullOrEmpty(where.PushToken)
        ).ConfigureAwait(false);
        //Valida si se encontraron Tokens
        NullException.ThrowIfNullOrEmpty(usersTokenNotification, "No se encontró Tokens válidos con los datos Ingresados.");
        return [.. usersTokenNotification];
    }

    /// <summary>
    /// Obtiene la implementación a usar
    /// </summary>
    /// <param name="notificationPushImplementationType"></param>
    /// <returns></returns>
    private static string GetImplementation(NotificationPushImplementationType notificationPushImplementationType)
     => notificationPushImplementationType switch
     {
         NotificationPushImplementationType.Firebase => $"{NotificationPushImplementationType.Firebase}",
         NotificationPushImplementationType.Huawei => $"{NotificationPushImplementationType.Huawei}",
         _ => throw new NotImplementedException($"No se encuentra implementación para :{notificationPushImplementationType}"),
     };
}