namespace LogicAdministratorApi.BusinessLogic.GymBranchHandler;

/// <summary>
/// Clase base para handlers de sucursal de gimnasio
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class GymBranchBase<TRequest, TResponse>(
    ILogger<GymBranchBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicAdministratorBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

