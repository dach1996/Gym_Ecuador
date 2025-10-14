using Common.PluginFactory.Interface;
using Common.Utils.Extensions;
using Common.WebJob.Model;
using LogicWebJob.Model.Request.Seat;
namespace WebJobs.Funtions.Queues;

/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
/// <returns></returns>
public abstract class QueueBase(
    ILogger<QueueBase> logger,
    IPluginFactory pluginFactory) : FunctionBase(logger, pluginFactory)
{

    /// <summary>
    /// Ejecuta el proceso mediante el envío por Mediator
    /// </summary>
    /// <param name="message"></param>
    /// <typeparam name="TRequest"></typeparam>
    /// <returns></returns>
    public async Task ExecuteQueueAsync<TRequest>(string message) where TRequest : IFunctionOperationRequest
         => await ExecuteFunctionAsync(message, async () =>
         {
             // Agregar el Correlation ID a los logs
             if (AppSettingsWebJob.CustomLog?.QueueMessage ?? false)
                 logger.LogInformation("Mensaje: {@Message}", message);
             var request = message.ToObject<TRequest>();
             var webJobContext = message.ToObject<WebJobContext>();
             request.WebJobContext = webJobContext;
             logger.LogInformation("Request: {@Request}", request.ToJson());
             await Mediator.Send(request).ConfigureAwait(false);
         });


}