namespace Common.Queue.Model.Response;
/// <summary>
/// Clase de respuesta de Envío de Queue
/// </summary>
public class SendQueueMessageResponse : QueueMessageResponseBase
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="popReceipt"></param>
    /// <returns></returns>
    public SendQueueMessageResponse(string messageId, string popReceipt) : base(true, string.Empty)
    {
        MessageId = messageId;
        PopReceipt = popReceipt;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="popReceipt"></param>
    /// <returns></returns>
    public SendQueueMessageResponse(string message) : base(false, message)
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="success"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    protected SendQueueMessageResponse(bool success, string message) : base(success, message)
    {
    }

    /// <summary>
    /// Id de Mensaje
    /// </summary>
    /// <value></value>
    public string MessageId { get; private set; }

    /// <summary>
    /// Cola
    /// </summary>
    /// <value></value>
    public string PopReceipt { get; private set; }
}