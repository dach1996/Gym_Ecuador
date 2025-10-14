using Common.Models.CatalogsTypeItems.Interface;

namespace LogicApi.BusinessLogic.CommonConfigurationHandler;
public abstract class CommonConfigurationBase<TRequest, TResponse>(
    ILogger<CommonConfigurationBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);


    protected ICataloguesType CataloguesType => PluginFactory.GetType<ICataloguesType>();

}
