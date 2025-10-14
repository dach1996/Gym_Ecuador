using Common.PushNotification.Model;
namespace LogicCommon.Model.Request.NotificationPush;
/// <summary>
/// Base de request
/// </summary>
public abstract class SendNotificationBaseRequest
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
    public NotificationAction? Action { get; set; }

    /// <summary>
    /// Información Adicional
    /// </summary>
    /// <value></value>
    public IDictionary<string, string> Data { get; set; }
}