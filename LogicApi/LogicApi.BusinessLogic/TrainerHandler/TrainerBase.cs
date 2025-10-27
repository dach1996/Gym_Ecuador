namespace LogicApi.BusinessLogic.TrainerHandler;

/// <summary>
/// Clase base para handlers de entrenador
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class TrainerBase<TRequest, TResponse>(
    ILogger<TrainerBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
