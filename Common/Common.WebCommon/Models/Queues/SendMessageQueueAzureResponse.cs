namespace Common.WebCommon.Models.Queues;
/// <summary>
/// Respuesta de Mensaje de Azure
/// </summary>
public class SendMessageQueueAzureResponse
{
    /// <summary>
    /// Id de Mensaje
    /// </summary>
    /// <value></value>
    public string MessageId { get; set; }

    /// <summary>
    /// Cola
    /// </summary>
    /// <value></value>
    public string PopReceipt { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="popReceipt"></param>
    public SendMessageQueueAzureResponse(string messageId, string popReceipt)
    {
        MessageId = messageId;
        PopReceipt = popReceipt;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public SendMessageQueueAzureResponse()
    {

    }
}