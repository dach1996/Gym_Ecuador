
namespace Common.PushNotification.Model;
/// <summary>
/// Notificación Token
/// </summary>
public class NotificationTokens : NotificationModelBase
{
    /// <summary>
    /// Diccionario de Tokens con implementación
    /// </summary>
    /// <value></value>
    public IEnumerable<NotificationTokenImplementationRequestModel> Tokens { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="title"></param>
    /// <param name="body"></param>
    /// <param name="tokens"></param>
    /// <param name="imageUrl"></param>
    /// <param name="redirectUrl"></param>
    /// <param name="action"></param>
    /// <param name="data"></param>
    public NotificationTokens(
        string title,
        string body,
        IEnumerable<NotificationTokenImplementationRequestModel> tokens,
        string imageUrl = null,
        string redirectUrl = null,
        NotificationAction? action = null,
        IDictionary<string, string> data = null
        )
    {
        Title = title;
        Body = body;
        ImageUrl = imageUrl;
        RedirectUrl = redirectUrl;
        Action = action;
        Tokens = tokens;
        Data = data;
    }

    /// <summary>
    /// Token y su implementación
    /// </summary>
    public class NotificationTokenImplementationRequestModel
    {
        /// <summary>
        /// Implementación
        /// </summary>
        public string Implementation { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }
}