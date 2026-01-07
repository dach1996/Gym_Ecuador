using System.Text.Json.Serialization;
using LogicCommon.Model.Response.NotificationPush;
namespace LogicCommon.Model.Request.NotificationPush;
/// <summary>
/// Request para Envío de Notificación Push
/// </summary>
public class SendNotificationPushUsersRequest : SendNotificationRequestBase, ICommonBaseRequest<NotificationPushResponse>
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
        CommonContextRequest commonContextRequest)
    {
        Title = title;
        Body = body;
        UsersId = usersId;
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
        CommonContextRequest commonContextRequest
        )
    {
        Title = title;
        Body = body;
        UsersId = [userId];
        CommonContextRequest = commonContextRequest;
    }
}

