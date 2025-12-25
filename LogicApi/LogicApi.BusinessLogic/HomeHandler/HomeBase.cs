namespace LogicApi.BusinessLogic.HomeHandler;

/// <summary>
/// Clase base para handlers de home/dashboard
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class HomeBase<TRequest, TResponse>(
    ILogger<HomeBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

