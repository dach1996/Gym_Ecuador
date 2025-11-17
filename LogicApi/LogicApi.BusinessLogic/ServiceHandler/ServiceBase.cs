namespace LogicApi.BusinessLogic.ServiceHandler;

/// <summary>
/// Clase base para handlers de servicios
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class ServiceBase<TRequest, TResponse>(
    ILogger<ServiceBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

