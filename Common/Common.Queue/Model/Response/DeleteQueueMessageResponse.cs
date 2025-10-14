namespace Common.Queue.Model.Response;
/// <summary>
/// Clase de respuesta de eliminación de Queue
/// </summary>
public class DeleteQueueMessageResponse(int success, int total)
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
    public static DeleteQueueMessageResponse Sucess(int success, int total)
     => new(success, total);


}