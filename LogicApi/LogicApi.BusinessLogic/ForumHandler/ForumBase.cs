namespace LogicApi.BusinessLogic.ForumHandler;

/// <summary>
/// Clase base para handlers de foro
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class ForumBase<TRequest, TResponse>(
    ILogger<ForumBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

