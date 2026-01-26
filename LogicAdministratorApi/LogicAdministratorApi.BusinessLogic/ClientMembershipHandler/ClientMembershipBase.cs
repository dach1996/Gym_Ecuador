namespace LogicAdministratorApi.BusinessLogic.ClientMembershipHandler;

/// <summary>
/// Clase base para handlers de membresías de cliente
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class ClientMembershipBase<TRequest, TResponse>(
    ILogger<ClientMembershipBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicAdministratorBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
