namespace LogicAdministratorApi.BusinessLogic.RoleHandler;

/// <summary>
/// Clase base para handlers de roles
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class RoleBase<TRequest, TResponse>(
    ILogger<RoleBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicAdministratorBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
