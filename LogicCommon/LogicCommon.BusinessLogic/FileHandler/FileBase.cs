global using Common.Blob;

namespace LogicCommon.BusinessLogic.FileHandler;
public abstract class FileBase<TRequest, TResponse>(
    ILogger<FileBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicCommonBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    protected IBlobBus BlobBus => PluginFactory.GetPlugin<IBlobBus>(AppSettings.BlobConfiguration?.CurrentImplementation, true);

    /// <summary>
    /// Abstracciòn de Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}