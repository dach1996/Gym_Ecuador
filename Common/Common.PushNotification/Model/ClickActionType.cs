namespace Common.PushNotification.Model;
/// <summary>
/// Acción de Click
/// </summary>
public enum NotificationAction
{
    /// <summary>
    /// Acción abrir Url
    /// </summary>
    OpenUrl,

    /// <summary>
    /// Acción abrir aplicación
    /// </summary>
    OpenApp,

    /// <summary>
    /// Solo Notificación
    /// </summary>
    Notification,

    /// <summary>
    /// Modal
    /// </summary>
    Modal,

    /// <summary>
    /// Evento
    /// </summary>
    Event,
}
