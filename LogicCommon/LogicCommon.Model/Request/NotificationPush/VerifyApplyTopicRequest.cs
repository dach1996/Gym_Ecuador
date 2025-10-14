using System.Text.Json.Serialization;

namespace LogicCommon.Model.Request.NotificationPush;

/// <summary>
/// Request para verificar si un usuario aplica a un tópico
/// </summary>
public class VerifyApplyTopicRequest : ICommonBaseRequest
{
    /// <summary>
    /// Id de Usuario
    /// </summary>
    /// <value></value>
    public int UserId { get; set; }

    /// <summary>
    /// Plataforma
    /// </summary>
    /// <value></value>
    public Platform Platform { get; set; }

    /// <summary>
    /// DeviceId
    /// </summary>
    /// <value></value>
    public string DeviceId { get; set; }

    /// <summary>
    /// ContextRequest
    /// </summary>
    /// <value></value>
    [JsonIgnore]
    public CommonContextRequest CommonContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="platform"></param>
    /// <param name="commonContextRequest"></param>

    public VerifyApplyTopicRequest(Platform platform, CommonContextRequest commonContextRequest)
    {
        Platform = platform;
        CommonContextRequest = commonContextRequest;
    }
}