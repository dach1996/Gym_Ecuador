namespace LogicApi.BusinessLogic.BranchEquipmentHandler;

/// <summary>
/// Clase base para handlers de equipos de sucursal
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class BranchEquipmentBase<TRequest, TResponse>(
    ILogger<BranchEquipmentBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

