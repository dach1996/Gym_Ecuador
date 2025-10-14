using Common.Queue.Interface;
using Common.Queue.Model.Enum;
using Common.Queue.Model.Request;
using Common.Queue.Model.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Common.Queue.Implementation;
/// <summary>
/// Clase base de Queue
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="logger"></param>
/// <param name="configuration"></param>
public abstract class QueueBase(
    ILogger<QueueBase> logger,
 IConfiguration configuration) : IQueue
{
    protected abstract QueueImplementationName QueueImplementationName { get; }
    protected readonly ILogger<QueueBase> Logger = logger;
    protected readonly IConfiguration Configuration = configuration;

    /// <summary>
    /// Envío de Queue
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public abstract Task<SendQueueMessageResponse> SendMessageAsync(SendQueueMessageRequest request);

    /// <summary>
    /// Eliminación de Queue
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public abstract Task<DeleteQueueMessageResponse> DeleteMessageAsync(DeleteQueueMessageRequest request);
}