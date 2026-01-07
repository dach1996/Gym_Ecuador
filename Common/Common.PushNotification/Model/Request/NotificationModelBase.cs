using Common.PushNotification.Model.Enum;

namespace Common.PushNotification.Model.Request;
/// <summary>
/// Clase base para notificaciones
/// </summary>
public abstract class NotificationRequestBase
{
    /// <summary>
    /// Título de nótificación
    /// </summary>
    /// <value></value>
    public string Title { get; set; }

    /// <summary>
    /// Cuerpo de nótificación
    /// </summary>
    /// <value></value>
    public string Body { get; set; }

    /// <summary>
    ///  Url a redirigir de nótificación
    /// </summary>
    /// <value></value>
    public string RedirectUrl { get; set; }

    /// <summary>
    /// Url imagen de nótificación
    /// </summary>
    /// <value></value>
    public string ImageUrl { get; set; }

    /// <summary>
    /// Acción de notificar nótificación
    /// </summary>
    /// <value></value>
    public ClickActionType? Action { get; set; }
}
