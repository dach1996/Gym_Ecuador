namespace LogicApi.BusinessLogic.MembershipHandler;

/// <summary>
/// Clase base para handlers de membresía
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class MembershipBase<TRequest, TResponse>(
    ILogger<MembershipBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
