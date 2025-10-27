namespace LogicApi.BusinessLogic.GymHandler;

/// <summary>
/// Clase base para handlers de gimnasio
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class GymBase<TRequest, TResponse>(
    ILogger<GymBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
