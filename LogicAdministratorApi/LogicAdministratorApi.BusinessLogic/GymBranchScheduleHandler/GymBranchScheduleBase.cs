namespace LogicAdministratorApi.BusinessLogic.GymBranchScheduleHandler;

/// <summary>
/// Clase base para handlers de horarios de sucursales
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class GymBranchScheduleBase<TRequest, TResponse>(
    ILogger<GymBranchScheduleBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicAdministratorBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

