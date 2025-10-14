
using System.Text.Json.Serialization;
using Common.Queue.Model.Enum;
using LogicCommon.Model.Response.Queue;

namespace LogicCommon.Model.Request.Queue;
/// <summary>
/// Constructor
/// </summary>
/// <typeparam name="DeleteMessageQueueItem"></typeparam>
public class DeleteMessageQueueRequest(IEnumerable<DeleteMessageQueueItem> deleteMessageQueueItems, CommonContextRequest commonContextRequest) : ICommonBaseRequest<DeleteMessageQueueResponse>
{

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest CommonContextRequest { get; set; } = commonContextRequest;

    /// <summary>
    /// Items
    /// </summary>
    /// <value></value>
    public IEnumerable<DeleteMessageQueueItem> DeleteMessageQueueItems { get; set; } = deleteMessageQueueItems;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="deleteMessageQueueItems"></param>
    /// <param name="commonContextRequest"></param>
    public DeleteMessageQueueRequest(DeleteMessageQueueItem deleteMessageQueueItems, CommonContextRequest commonContextRequest)
        : this([deleteMessageQueueItems], commonContextRequest) { }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="queueTemplateName"></param>
    /// <param name="messageId"></param>
    /// <param name="popReceipt"></param>
    /// <param name="commonContextRequest"></param>
    public DeleteMessageQueueRequest(QueueTemplateName queueTemplateName, string messageId, string popReceipt, CommonContextRequest commonContextRequest)
    : this([new(queueTemplateName, messageId, popReceipt)], commonContextRequest) { }
}

/// <summary>
/// Item de eliminar Queue
/// </summary>
public class DeleteMessageQueueItem(QueueTemplateName queueTemplateName, string messageId, string popReceipt)
{
    /// <summary>
    /// Nombre del Queue
    /// </summary>
    /// <value></value>
    public QueueTemplateName QueueTemplateName { get; } = queueTemplateName;

    /// <summary>
    /// Id de Mensaje
    /// </summary>
    /// <value></value>
    public string MessageId { get; private set; } = messageId;

    /// <summary>
    /// Cola
    /// </summary>
    /// <value></value>
    public string PopReceipt { get; private set; } = popReceipt;
}