using LogicCommon.Model.Request.File;
using LogicCommon.Model.Response.File;
namespace LogicCommon.BusinessLogic.FileHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateBlobFileHandler(
    ILogger<UpdateBlobFileHandler> logger,
    IPluginFactory pluginFactory) : FileBase<UpdateBlobFileRequest, FileResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<FileResponse> Handle(UpdateBlobFileRequest request, CancellationToken cancellationToken)
    {
        using var stream = new MemoryStream(request.File);
        var file = await BlobBus.UpdateAndGetFileAsync(request.FileName, request.Path, stream, request.ReplaceIfExist ?? true).ConfigureAwait(false);
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
