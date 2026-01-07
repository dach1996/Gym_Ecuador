using Common.PushNotification.Model;
using Common.PushNotification.Model.Request;
namespace Common.PushNotification;
/// <summary>
/// Interfaz de notificaciones 
/// </summary>
public interface IPushNotification
{
    /// <summary>
    /// Envíar notificación por token
    /// </summary>
    /// <param name="notificationToken"></param>
    /// <returns></returns>
    Task<NotificationResponse> SendNotificationAsync(NotificationByTokenRequest notificationTokens);

    /// <summary>
    /// Enviar Notificación por Topic
    /// </summary>
    /// <param name="notificationTopic"></param>
    /// <returns></returns>
    Task<NotificationResponse> SendNotificationAsync(NotificationByTopicRequest notificationTopic);
}
