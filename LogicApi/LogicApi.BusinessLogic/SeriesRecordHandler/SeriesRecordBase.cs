namespace LogicApi.BusinessLogic.SeriesRecordHandler;

/// <summary>
/// Clase base para handlers de registro de series
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class SeriesRecordBase<TRequest, TResponse>(
    ILogger<SeriesRecordBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
