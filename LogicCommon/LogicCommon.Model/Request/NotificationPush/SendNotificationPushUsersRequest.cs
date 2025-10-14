using System.Text.Json.Serialization;
using Common.PushNotification.Model;
using LogicCommon.Model.Response.NotificationPush;
namespace LogicCommon.Model.Request.NotificationPush;
/// <summary>
/// Request para Envío de Notificación Push
/// </summary>
public class SendNotificationPushUsersRequest : SendNotificationBaseRequest, ICommonBaseRequest<NotificationPushResponse>
{
    /// <summary>
    /// UserIds
    /// </summary>
    /// <value></value>
    public IEnumerable<int> UsersId { get; set; }

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
    /// <param name="usersId"></param>
    /// <param name="action"></param>
    /// <param name="commonContextRequest"></param>
    public SendNotificationPushUsersRequest(
        string title,
        string body,
        IEnumerable<int> usersId,
        NotificationAction action,
        CommonContextRequest commonContextRequest)
    {
        Title = title;
        Body = body;
        UsersId = usersId;
        Action = action;
        CommonContextRequest = commonContextRequest;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="title"></param>
    /// <param name="body"></param>
    /// <param name="token"></param>
    /// <param name="action"></param>
    /// <param name="commonContextRequest"></param>
    public SendNotificationPushUsersRequest(
        string title,
        string body,
        int userId,
        NotificationAction action,
        CommonContextRequest commonContextRequest
        )
    {
        Title = title;
        Body = body;
        UsersId = [userId];
        Action = action;
        CommonContextRequest = commonContextRequest;
    }
}

