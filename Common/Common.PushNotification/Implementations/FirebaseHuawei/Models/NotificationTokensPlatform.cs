using Common.PushNotification.Model.Request;

namespace Common.PushNotification.Implementations.FirebaseHuawei.Models;
/// <summary>
/// Notificación Token
/// </summary>
internal class NotificationTokensPlatformRequest : NotificationRequestBase
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
    public NotificationTokensPlatformRequest(string title, string body, IEnumerable<string> tokens)
    {
        Title = title;
        Body = body;
        Tokens = tokens;
    }
}
