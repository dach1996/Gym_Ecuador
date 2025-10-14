using Common.Queue.Model.Request;
using Common.Queue.Model.Response;

namespace Common.Queue.Interface;

/// <summary>
/// Interface de Queue
/// </summary>
public interface IQueue
{
    /// <summary>
    /// Envío de Mensaje
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<SendQueueMessageResponse> SendMessageAsync(SendQueueMessageRequest request);

    /// <summary>
    /// Envío de Mensaje
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<DeleteQueueMessageResponse> DeleteMessageAsync(DeleteQueueMessageRequest request);
}