namespace Common.Queue.Model.Response;
/// <summary>
/// Clase de respuesta Base de Queue
/// </summary>
public class QueueMessageResponseBase
{
    /// <summary>
    /// Estado
    /// </summary>
    /// <value></value>
    public bool Success { get; private set; }

    /// <summary>
    /// Mensaje
    /// </summary>
    /// <value></value>
    public string Message { get; private set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="success"></param>
    /// <param name="message"></param>
    protected QueueMessageResponseBase(bool success, string message)
    {
        Success = success;
        Message = message;
    }

}