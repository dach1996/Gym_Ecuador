using Common.PluginFactory.Interface;
using LogicWebJob.Model.Request.Seat;
using Microsoft.Azure.WebJobs;

namespace WebJobs.Funtions.Queues;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class ExpiredOrderQueue(
    ILogger<ExpiredOrderQueue> logger,
    IPluginFactory pluginFactory) : QueueBase(logger, pluginFactory)
{
    protected override string FunctionName => nameof(ExpiredOrderQueue);

    [FunctionName(nameof(ExpiredOrderQueue))]
    public async Task Receive([QueueTrigger("expiredorder")] string message)
     => await ExecuteQueueAsync<ExpiredOrderRequest>(message).ConfigureAwait(false);
}