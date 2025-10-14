namespace LogicWebJob.BusinessLogic.OrderHandler;

/// <summary>
/// Clase base de Web Jobs para Ordenes
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public abstract class OrderBase<TRequest, TResponse>(
    ILogger<OrderBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicWebJobBase(logger, pluginFactory),
IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}