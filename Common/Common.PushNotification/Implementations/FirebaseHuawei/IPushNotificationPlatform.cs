using Common.PushNotification.Implementations.FirebaseHuawei.Models;
using Common.PushNotification.Model;
using Common.PushNotification.Model.Request;
namespace Common.PushNotification.Implementations.FirebaseHuawei;
/// <summary>
/// Interfaz de notificaciones 
/// </summary>
internal interface IPushNotificationPlatform
{
    /// <summary>
    /// Envíar notificación por token
    /// </summary>
    /// <param name="notificationToken"></param>
    /// <returns></returns>
    Task<NotificationResponse> SendNotificationAsync(NotificationTokensPlatformRequest notificationToken);

    /// <summary>
    /// Enviar Notificación por Topic
    /// </summary>
    /// <param name="notificationTopic"></param>
    /// <returns></returns>
    Task<NotificationResponse> SendNotificationAsync(NotificationByTopicRequest notificationTopic);
}
