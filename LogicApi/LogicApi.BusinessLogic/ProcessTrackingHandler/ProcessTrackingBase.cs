namespace LogicApi.BusinessLogic.ProcessTrackingHandler;

/// <summary>
/// Clase base para handlers de seguimiento de procesos
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class ProcessTrackingBase<TRequest, TResponse>(
    ILogger<ProcessTrackingBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
