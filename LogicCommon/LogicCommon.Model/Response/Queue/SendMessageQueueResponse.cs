namespace LogicCommon.Model.Response.Queue;
/// <summary>
/// Respuesta de Envío de Queue
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="success"></param>
/// <param name="message"></param>
/// <param name="messageId"></param>
/// <param name="popReceipt"></param>
/// <param name="internalIdentifier"></param>
/// <param name="typeCode"></param>
public class SendMessageQueueResponse(
    bool success,
    string message,
    string messageId,
    string popReceipt,
    Guid internalIdentifier,
    byte typeCode)
{
    /// <summary>
    /// Estado de Envío
    /// </summary>
    /// <value></value>
    public bool Success { get; private set; } = success;

    /// <summary>
    /// Mensaje
    /// </summary>
    /// <value></value>
    public string Message { get; private set; } = message;

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

    /// <summary>
    /// Cola
    /// </summary>
    /// <value></value>
    public Guid InternalIdentifier { get; private set; } = internalIdentifier;

    /// <summary>
    /// Tipo de Código
    /// </summary>
    /// <value></value>
    public byte TypeCode { get; private set; } = typeCode;
}