using Common.PushNotification.Model;
using Common.Utils.CustomExceptions;
using Common.Utils.Extensions;
using LogicCommon.Abstraction.Interfaces.Notification;
using LogicCommon.Model.Request.NotificationPush;
using LogicCommon.Model.Response.NotificationPush;
using PersistenceDb.Models.Administration;
using PersistenceDb.Models.Enums;
namespace LogicCommon.BusinessLogic.NotificationHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class SendNotificationPushTopicHandler(
    ILogger<SendNotificationPushTopicHandler> logger,
    IPluginFactory pluginFactory) : NotificationPushHandlerBase<SendNotificationPushTopicRequest, NotificationPushResponse>(logger, pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<NotificationPushResponse> Handle(SendNotificationPushTopicRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(request, async () =>
        {
            var notificationPushRespository = await UnitOfWork.NotificationPushRepository.AddAsync(new()
            {
                Title = request.Title,
                Description = request.Body,
                RegisterDate = Clock.Now(),
                PushNotificationType = PushNotificationType.Topic,
                PushNotificationValue = $"{request.Topic}",
            }).ConfigureAwait(false);
            //Obtiene la implementación
            var topicNotificationImplementation = PluginFactory.GetPlugin<ITopicNotification>($"{request.Topic}");
            //Con la implementación obtiene los usuarios a enviar
            var userNotifications = await topicNotificationImplementation.GetUserNotificationsApplyTopicAsync(request).ConfigureAwait(false);
            //Valida si se encontraron Tokens
            NullException.ThrowIfNullOrEmpty(userNotifications, nameof(userNotifications), nameof(SendNotificationPushTopicHandler));

            //Envía Notificaciones
            var notificationPushResponse = (await PushNotificationPlatform.SendNotificationAsync(new NotificationTopic(
                    request.Title,
                    request.Body,
                    $"{request.Topic}")).ConfigureAwait(false))
                ?.NotificationItems?.FirstOrDefault();
            var notificationsPushDeviceRepository = userNotifications.GroupBy(group => group.UserId)
            .Select(select => new NotificationPushUser
            {
                UserId = select.Key,
                PushNotificationId = notificationPushRespository.Id,
                NotificationPushUserDevices = [.. select.Select(select => new NotificationPushUserDevice
                {
                    DeviceId = select.DeviceId,
                    NotificationPushUserId = notificationPushRespository.Id,
                    StatusNotification = notificationPushResponse.IsSuccess ? StatusNotification.Sended : StatusNotification.Error,
                    AdditionalInformation = notificationPushResponse.IsSuccess ? notificationPushResponse.MessageId : notificationPushResponse.Message
                })]
            });
            //Almacena las notificaciones Push del usuario
            await UnitOfWork.NotificationPushUserRepository.AddRangeAsync(notificationsPushDeviceRepository).ConfigureAwait(false);
            return new NotificationPushResponse(
                notificationsPushDeviceRepository.ToDictionary(
                    key => key.UserId,
                    value => new NotificationPushResponse.NotificationPushUserResponse(
                        value.NotificationPushUserDevices.Count,
                        value.NotificationPushUserDevices.Count(countWhere => countWhere.StatusNotification == StatusNotification.Sended)
                    )
                )
            );
        }).ConfigureAwait(false);
}