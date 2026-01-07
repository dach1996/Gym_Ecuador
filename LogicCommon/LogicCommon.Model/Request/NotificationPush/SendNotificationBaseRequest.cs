using Common.PushNotification.Model;
namespace LogicCommon.Model.Request.NotificationPush;
/// <summary>
/// Base de request
/// </summary>
public abstract class SendNotificationRequestBase
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
    /// Url imagen de nótificación
    /// </summary>
    /// <value></value>
    public string ImageUrl { get; set; }

    /// <summary>
    /// Información adicional
    /// </summary>
    /// <value></value>
    public IDictionary<string, string> Data { get; set; }
}