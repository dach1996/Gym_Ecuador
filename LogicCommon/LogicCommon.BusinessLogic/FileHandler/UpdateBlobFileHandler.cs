using Common.Blob;
using LogicCommon.Model.Request.File;
using LogicCommon.Model.Response.File;
using PersistenceDb.Models.Administration;
namespace LogicCommon.BusinessLogic.FileHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateBlobFileHandler(
    ILogger<UpdateBlobFileHandler> logger,
    IPluginFactory pluginFactory) : FileBase<UpdateBlobFileRequest, UpdateFileResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<UpdateFileResponse> Handle(UpdateBlobFileRequest request, CancellationToken cancellationToken)
      => await ExecuteHandlerAsync(request, async () =>
    {
        var fileBasePaths = await GetFileBasePathCacheInformationByPathCodeAsync(request.PathCode).ConfigureAwait(false);
        using var stream = new MemoryStream(request.File);
        var path = request.Path ?? fileBasePaths.FileDirectoryPath;
        var updateFileResponse = await PluginFactory.GetPlugin<IBlobBus>(fileBasePaths.Implementation, true).UpdateFileAsync(request.FileName, path, stream, request.ReplaceIfExist ?? true).ConfigureAwait(false);
        var filePersistence = await UnitOfWork.FileRepository.AddAsync(new FilePersistence
        {
            Name = updateFileResponse.FileName,
            Path = updateFileResponse.Path,
            DateRegister = Now,
            State = true,
            FileBasePathId = fileBasePaths.Id
        });
        return new UpdateFileResponse
        {
            FileName = updateFileResponse.FileName,
            Path = updateFileResponse.Path,
            Id = filePersistence.Id
        };
    }).ConfigureAwait(false);
}
