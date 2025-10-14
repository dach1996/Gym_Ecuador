
using System.Text.Json.Serialization;
using Common.Queue.Model.Template;
using LogicCommon.Model.Response.Queue;

namespace LogicCommon.Model.Request.Queue;
/// <summary>
/// Envìo de Queue
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="queueTemplate"></param>
/// <param name="commonContextRequest"></param>
/// <param name="delaySeconds"></param>
public class SendMessageQueueRequest(IQueueTemplate queueTemplate, CommonContextRequest commonContextRequest, int delaySeconds = 0) : ICommonBaseRequest<SendMessageQueueResponse>
{
    /// <summary>
    /// Template de Queue
    /// </summary>
    /// <value></value>
    public IQueueTemplate QueueTemplate { get; private set; } = queueTemplate;

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest CommonContextRequest { get; set; } = commonContextRequest;

    /// <summary>
    /// Segundos de tardansa
    /// </summary>
    /// <value></value>
    public int DelaySeconds { get; set; } = delaySeconds;
}