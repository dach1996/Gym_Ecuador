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
    IPluginFactory pluginFactory) : FileBase<DownloadBlobFileRequest, FileResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public  override async Task<FileResponse> Handle(DownloadBlobFileRequest request, CancellationToken cancellationToken)
    {
        var file = await BlobBus.DownloadFileAsync(request.FileName, request.Path).ConfigureAwait(false);
        return new FileResponse
        {
            Content = file.Content,
            ContentType = file.ContentType,
            FileName = file.FileName,
            LastModified = file.LastModified,
            Length = file.Length,
            Url = file.Url
        };
    }
}
