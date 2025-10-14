using Common.PushNotification.Model;
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
    Task<NotificationResponse> SendNotificationAsync(NotificationTokens notificationTokens);

    /// <summary>
    /// Enviar Notificación por Topic
    /// </summary>
    /// <param name="notificationTopic"></param>
    /// <returns></returns>
    Task<NotificationResponse> SendNotificationAsync(NotificationTopic notificationTopic);
}
