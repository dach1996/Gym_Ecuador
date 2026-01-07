using LogicCommon.Model.Response.NotificationPush;
using System.Text.Json.Serialization;
namespace LogicCommon.Model.Request.NotificationPush;
/// <summary>
/// Request para Envío de Notificación Push
/// </summary>
public class SendNotificationPushTopicRequest : SendNotificationRequestBase, ICommonBaseRequest<NotificationPushResponse>
{
    /// <summary>
    /// Topic
    /// </summary>
    /// <value></value>
    public TopicType Topic { get; set; }

    /// <summary>
    /// ContextRequest
    /// </summary>
    /// <value></value>
    [JsonIgnore]
    public CommonContextRequest CommonContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="title"></param>
    /// <param name="body"></param>
    /// <param name="topic"></param>
    /// <param name="NotificationPushCategory"></param>
    /// <param name="commonContextRequest"></param>
    public SendNotificationPushTopicRequest(
        string title,
        string body,
        TopicType topic,
        CommonContextRequest commonContextRequest
        )
    {
        Title = title;
        Body = body;
        Topic = topic;
        CommonContextRequest = commonContextRequest;
    }
}