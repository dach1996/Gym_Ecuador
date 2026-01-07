namespace Common.PushNotification.Model.Request;
/// <summary>
/// Notificación Token
/// </summary>
public class NotificationByTokenRequest : NotificationRequestBase
{
    /// <summary>
    /// Diccionario de Tokens con consulta sobre Servicios Google
    /// </summary>
    /// <value></value>
    public Dictionary<BrandNotificationType, List<string>> Tokens { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="title"></param>
    /// <param name="body"></param>
    /// <param name="tokens"></param>
    public NotificationByTokenRequest(string title, string body, Dictionary<BrandNotificationType, List<string>> tokens)
    {
        Title = title;
        Body = body;
        Tokens = tokens;
    }

    public enum BrandNotificationType
    {
        /// <summary>
        /// Notificación para Brand
        /// </summary>
        AndroidIOS = 1,
        /// <summary>
        /// Notificación para Notification
        /// </summary>
        Huawei = 2
    }
}
