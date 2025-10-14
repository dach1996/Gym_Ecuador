using Common.EventHub;
using Common.Queue.Interface;

namespace LogicCommon.BusinessLogic.EventHandler;
public abstract class EventBase<TRequest, TResponse> : BusinessLogicCommonBase,
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    protected readonly IEventHub EventHub;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="pluginFactory"></param>
    protected EventBase(
        ILogger<EventBase<TRequest, TResponse>> logger,
        IPluginFactory pluginFactory)
        : base(
            logger,
            pluginFactory)
    {
        EventHub = PluginFactory.GetPlugin<IEventHub>(AppSettings.EventHubConfiguration?.CurrentImplementation, true);
    }

    /// <summary>
    /// Abstracciòn de Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}