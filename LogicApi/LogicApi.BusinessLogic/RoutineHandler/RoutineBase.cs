namespace LogicApi.BusinessLogic.RoutineHandler;

/// <summary>
/// Clase base para handlers de rutinas
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class RoutineBase<TRequest, TResponse>(
    ILogger<RoutineBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
