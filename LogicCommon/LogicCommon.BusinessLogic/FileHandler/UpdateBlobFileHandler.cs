using Common.Blob;
using Common.Blob.Models.Request;
using Common.Utils;
using Common.Utils.ImageTools;
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
        var updateFileRequests = request.Items.Select(async select =>
        {
            var optimizedImage = select.FileName?.IsImage() ?? false ? await ImageManagement.OptimizeImageAsync(select.File).ConfigureAwait(false) : select.File;
            return new UpdateFileItemRequest
            {
                FileName = select.FileName,
                File = optimizedImage,
                ReplaceIfExist = select.ReplaceIfExist ?? true
            };
        });
        var updateFileRequestsList = await Task.WhenAll(updateFileRequests).ConfigureAwait(false);
        var updateFileResponse = await PluginFactory.GetPlugin<IBlobBus>(fileBasePaths.Implementation, true)
            .UpdateFileAsync(new(request.FolderPath ?? fileBasePaths.FileDirectoryPath, [.. updateFileRequestsList])).ConfigureAwait(false);
        var responseFiles = updateFileResponse.Items
        .Where(where => where.Success)
        .Select(where => new FilePersistence
        {
            Name = where.FileName,
            Path = where.Path,
            DateRegister = Now,
            State = true,
            FileBasePathId = fileBasePaths.Id,
            Guid = Guid.NewGuid()
        }).ToList();

        var filePersistence = await UnitOfWork.FileRepository.AddRangeIdentityAsync(responseFiles).ConfigureAwait(false);
        return new UpdateFileResponse
        {
            Items = [.. filePersistence.Select(select => new UpdateFileItemResponse
            {
                FileName = select.Name,
                Path = select.Path,
                Id = select.Id
            })]
        };
    }).ConfigureAwait(false);
}
