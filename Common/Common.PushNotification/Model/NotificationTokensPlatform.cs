
namespace Common.PushNotification.Model;
/// <summary>
/// Notificación Token
/// </summary>
public class NotificationTokensPlatform : NotificationModelBase
{
    /// <summary>
    /// Token
    /// </summary>
    /// <value></value>
    public IEnumerable<string> Tokens { get; set; }

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
    public NotificationTokensPlatform(
        string title,
        string body,
        IEnumerable<string> tokens,
        string imageUrl = null,
        string redirectUrl = null,
        NotificationAction? action = null,
        IDictionary<string, string> data = null
        )
    {
        Title = title;
        Body = body;
        Tokens = tokens;
        ImageUrl = imageUrl;
        RedirectUrl = redirectUrl;
        Action = action;
        Data = data;
    }
}
