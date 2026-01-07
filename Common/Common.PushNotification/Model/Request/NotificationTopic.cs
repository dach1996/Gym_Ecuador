namespace Common.PushNotification.Model.Request;
/// <summary>
/// Notificación por Topic
/// </summary>
public class NotificationByTopicRequest : NotificationRequestBase
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
    /// <param name="Topic"></param>
    public NotificationByTopicRequest(string title, string body, string topic)
    {
        Title = title;
        Body = body;
        Topic = topic;
    }
}
