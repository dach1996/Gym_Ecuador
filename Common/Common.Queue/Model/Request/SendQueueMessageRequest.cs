
namespace Common.Queue.Model.Request;
/// <summary>
/// Mensaje de Queue
/// </summary>
public class SendQueueMessageRequest
{
    /// <summary>
    /// Queue Name
    /// </summary>
    /// <value></value>
    public string QueueName { get; private set; }

    /// <summary>
    /// Mensaje
    /// </summary>
    /// <value></value>
    public string Message { get; private set; }

    /// <summary>
    /// Segundos de retraso
    /// </summary>
    /// <value></value>
    public int DelaySeconds { get; private set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="queueName"></param>
    /// <param name="message"></param>
    /// <param name="delaySeconds"></param>
    public SendQueueMessageRequest(string queueName, string message, int delaySeconds = 0)
    {
        QueueName = queueName;
        Message = message;
        DelaySeconds = delaySeconds;
    }

}