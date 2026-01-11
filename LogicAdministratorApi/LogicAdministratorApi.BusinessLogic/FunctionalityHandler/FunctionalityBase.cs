namespace LogicAdministratorApi.BusinessLogic.FunctionalityHandler;

/// <summary>
/// Clase base para handlers de funcionalidades
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class FunctionalityBase<TRequest, TResponse>(
    ILogger<FunctionalityBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicAdministratorBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
