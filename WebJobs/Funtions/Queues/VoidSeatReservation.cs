using Common.PluginFactory.Interface;
using LogicWebJob.Model.Request.Seat;
using Microsoft.Azure.WebJobs;

namespace WebJobs.Funtions.Queues;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class VoidSeatReservation(
    ILogger<VoidSeatReservation> logger,
    IPluginFactory pluginFactory) : QueueBase(logger, pluginFactory)
{
    protected override string FunctionName => nameof(VoidSeatReservation);

    [FunctionName(nameof(VoidSeatReservation))]
    public async Task Receive([QueueTrigger("voidseatreservation")] string message)
     => await ExecuteQueueAsync<VoidSeatReservationRequest>(message).ConfigureAwait(false);
}