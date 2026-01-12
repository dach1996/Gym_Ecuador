namespace LogicAdministratorApi.BusinessLogic.FunctionHandler;

/// <summary>
/// Clase base para handlers de funciones
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class FunctionBase<TRequest, TResponse>(
    ILogger<FunctionBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicAdministratorBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
