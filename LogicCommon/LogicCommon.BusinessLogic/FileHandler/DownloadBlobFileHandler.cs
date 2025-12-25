using Common.Blob;
using LogicApi.Model.Request.File;
using LogicCommon.Model.Response.File;

namespace LogicCommon.BusinessLogic.FileHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class DownloadBlobFileHandler(
    ILogger<DownloadBlobFileHandler> logger,
    IPluginFactory pluginFactory) : FileBase<DownloadBlobFileRequest, DownloadFileResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<DownloadFileResponse> Handle(DownloadBlobFileRequest request, CancellationToken cancellationToken)
    => await ExecuteHandlerAsync(request, async () =>
    {
        var file = await PluginFactory.GetPlugin<IBlobBus>(request.Implementation, true).DownloadFileAsync(request.FileName, request.Path).ConfigureAwait(false);
        return new DownloadFileResponse
        {
            FileName = file.FileName,
            Path = request.Path,
            Content = file.Content
        };
    }).ConfigureAwait(false);
}
