namespace LogicApi.BusinessLogic.ExerciseHandler;

/// <summary>
/// Clase base para handlers de ejercicios
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class ExerciseBase<TRequest, TResponse>(
    ILogger<ExerciseBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
