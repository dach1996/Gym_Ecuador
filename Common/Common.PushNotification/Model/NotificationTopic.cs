namespace Common.PushNotification.Model;
/// <summary>
/// Notificación por Topic
/// </summary>
public class NotificationTopic : NotificationModelBase
{
    /// <summary>
    /// Topic
    /// </summary>
    /// <value></value>
    public string Topic { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="title"></param>
    /// <param name="body"></param>
    /// <param name="topic"></param>
    /// <param name="imageUrl"></param>
    /// <param name="redirectUrl"></param>
    /// <param name="action"></param>
    /// <param name="data"></param>
    public NotificationTopic(
        string title,
        string body,
        string topic,
        string imageUrl = null,
        string redirectUrl = null,
        NotificationAction? action = null,
        IDictionary<string, string> data = null
        )
    {
        Title = title;
        Body = body;
        Topic = topic;
        ImageUrl = imageUrl;
        RedirectUrl = redirectUrl;
        Action = action;
        Data = data;
    }
}
