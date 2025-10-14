namespace LogicCommon.Model.Response.Queue;
/// <summary>
/// Respuesta de eliminar mensaje de Queue
/// </summary>
public class DeleteMessageQueueResponse(int success, int total)
{
    /// <summary>
    /// Correctos 
    /// </summary>
    /// <value></value>
    public int Success { get; private set; } = success;

    /// <summary>
    /// Total
    /// </summary>
    /// <value></value>
    public int Total { get; private set; } = total;

    /// <summary>
    /// Error
    /// </summary>
    /// <value></value>
    public int Error { get => Total - Success; }

    /// <summary>
    /// Constructor con éxito
    /// </summary>
    /// <returns></returns>
    public static DeleteMessageQueueResponse SuccessResponse(int success, int total)
     => new(success, total);

    /// <summary>
    /// Constructor con Error
    /// </summary>
    /// <returns></returns>
    public static DeleteMessageQueueResponse FailResponse(int total)
     => new(0, total);
}