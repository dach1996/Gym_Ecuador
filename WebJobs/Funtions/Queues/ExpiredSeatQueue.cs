using Common.PluginFactory.Interface;
using LogicWebJob.Model.Request.Seat;
using Microsoft.Azure.WebJobs;

namespace WebJobs.Funtions.Queues;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class ExpiredSeatQueue(
    ILogger<ExpiredSeatQueue> logger,
    IPluginFactory pluginFactory) : QueueBase(logger, pluginFactory)
{
    protected override string FunctionName => nameof(ExpiredSeatQueue);

    [FunctionName(nameof(ExpiredSeatQueue))]
    public async Task Receive([QueueTrigger("expiredseat")] string message)
     => await ExecuteQueueAsync<ExpireSeatRequest>(message).ConfigureAwait(false);
}