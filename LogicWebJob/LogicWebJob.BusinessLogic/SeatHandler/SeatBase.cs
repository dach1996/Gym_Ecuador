namespace LogicWebJob.BusinessLogic.SeatHandler;

/// <summary>
/// Clase base de Web Jobs para Asientos
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
/// <remarks>
/// Clase Base para verificar asientos
/// </remarks>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
/// <returns></returns>
public abstract class SeatBase<TRequest, TResponse>(
    ILogger<SeatBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicWebJobBase(logger, pluginFactory),
IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

}