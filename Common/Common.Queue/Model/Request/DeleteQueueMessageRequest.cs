
namespace Common.Queue.Model.Request;
/// <summary>
/// Mensaje de Queue
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="deleteQueueMessageItems"></param>
public class DeleteQueueMessageRequest(IEnumerable<DeleteQueueMessageItem> deleteQueueMessageItems)
{
    /// <summary>
    /// Items a Eliminar
    /// </summary>
    /// <value></value>
    public IEnumerable<DeleteQueueMessageItem> DeleteQueueMessageItems { get; set; } = deleteQueueMessageItems;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="deleteQueueMessageItem"></param>
    public DeleteQueueMessageRequest(DeleteQueueMessageItem deleteQueueMessageItem) : this([deleteQueueMessageItem]) { }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="queueName"></param>
    /// <param name="messageId"></param>
    /// <param name="popReceipt"></param>
    /// <returns></returns>
    public DeleteQueueMessageRequest(string queueName, string messageId, string popReceipt) : this([new(queueName, messageId, popReceipt)]) { }
}

/// <summary>
/// Constructor
/// </summary>
/// <param name="queueName"></param>
/// <param name="messageId"></param>
/// <param name="popReceipt"></param>
public class DeleteQueueMessageItem(string queueName, string messageId, string popReceipt)
{
    /// <summary>
    /// Nombre del Queue
    /// </summary>
    /// <value></value>
    public string QueueName { get; set; } = queueName;

    /// <summary>
    /// Id de Mensaje
    /// </summary>
    /// <value></value>
    public string MessageId { get; set; } = messageId;

    /// <summary>
    /// Cola
    /// </summary>
    /// <value></value>
    public string PopReceipt { get; set; } = popReceipt;
}