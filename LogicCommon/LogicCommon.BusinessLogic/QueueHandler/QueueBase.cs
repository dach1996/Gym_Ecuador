using Common.Queue.Interface;

namespace LogicCommon.BusinessLogic.QueueHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class QueueBase<TRequest, TResponse>(
    ILogger<QueueBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicCommonBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    protected  IQueue Queue => PluginFactory.GetPlugin<IQueue>(AppSettings.QueueConfiguration?.CurrentImplementation, true);

    /// <summary>
    /// Abstracciòn de Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}