namespace LogicApi.BusinessLogic.ArticleHandler;

/// <summary>
/// Clase base para handlers de artículos
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class ArticleBase<TRequest, TResponse>(
    ILogger<ArticleBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

