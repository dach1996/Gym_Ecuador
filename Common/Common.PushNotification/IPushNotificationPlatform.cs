using Common.PushNotification.Model;
namespace Common.PushNotification;
/// <summary>
/// Interfaz de notificaciones 
/// </summary>
interface IPushNotificationPlatform
{
    /// <summary>
    /// Envíar notificación por token
    /// </summary>
    /// <param name="notificationToken"></param>
    /// <returns></returns>
    Task<NotificationResponse> SendNotificationAsync(NotificationTokensPlatform notificationToken);

    /// <summary>
    /// Enviar Notificación por Topic
    /// </summary>
    /// <param name="notificationTopic"></param>
    /// <returns></returns>
    Task<NotificationResponse> SendNotificationAsync(NotificationTopic notificationTopic);
}
