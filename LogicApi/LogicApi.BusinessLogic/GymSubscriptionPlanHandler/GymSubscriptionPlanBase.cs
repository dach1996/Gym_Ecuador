namespace LogicApi.BusinessLogic.GymSubscriptionPlanHandler;

/// <summary>
/// Clase base para handlers de planes de suscripción
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class GymSubscriptionPlanBase<TRequest, TResponse>(
    ILogger<GymSubscriptionPlanBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

