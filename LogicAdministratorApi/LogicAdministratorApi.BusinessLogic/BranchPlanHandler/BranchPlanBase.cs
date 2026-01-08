namespace LogicAdministratorApi.BusinessLogic.BranchPlanHandler;

/// <summary>
/// Clase base para handlers de plan de sucursal
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class BranchPlanBase<TRequest, TResponse>(
    ILogger<BranchPlanBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicAdministratorBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

