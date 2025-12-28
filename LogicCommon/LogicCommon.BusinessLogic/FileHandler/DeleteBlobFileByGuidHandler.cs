using Common.Blob;
using Common.Blob.Models.Request;
using LogicCommon.Model.Request.File;
using LogicCommon.Model.Response.File;

namespace LogicCommon.BusinessLogic.FileHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class DeleteBlobFileByGuidHandler(
    ILogger<DeleteBlobFileByGuidHandler> logger,
    IPluginFactory pluginFactory) : FileBase<DeleteBlobFileByGuidRequest, DeleteFileResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<DeleteFileResponse> Handle(DeleteBlobFileByGuidRequest request, CancellationToken cancellationToken)
    => await ExecuteHandlerAsync(request, async () =>
    {
        var filePersistence = await UnitOfWork.FileRepository.GetGenericAsync(
            select => new
            {
                select.Name,
                select.Guid,
                select.Id,
                select.Path,
                select.FileBasePath.Implementation
            },
            where => request.Guids.Contains(where.Guid)).ConfigureAwait(false);
        List<DeleteFileItemResponse> items = [];
        foreach (var filesGroup in filePersistence.GroupBy(group => group.Implementation))
        {

            var response = await PluginFactory.GetPlugin<IBlobBus>(filesGroup.Key, true)
                     .DeleteFileAsync(new DeleteFileRequest([.. filesGroup.Select(
                         select => {
                           return new DeleteFileItemRequest
                            {
                                FullPathName = select.Path,
                                Identifier = select.Guid
                            };
                         })])).ConfigureAwait(false);
            var itemsResponse = response.Items.Join(
                filesGroup,
                item => item.Identifier,
                file => file.Guid, (
                    item, file) => new DeleteFileItemResponse
                    {
                        Guid = file.Guid,
                        Id = file.Id,
                        Success = item.Success
                    });
            items.AddRange(itemsResponse);
        }
        var itemsSuccess = items.Where(item => item.Success).Select(item => item.Id).ToList();
        await UnitOfWork.FileRepository.UpdateByAsync((select => select.State, false), where => itemsSuccess.Contains(where.Id)).ConfigureAwait(false);
        return new DeleteFileResponse(items);
    }).ConfigureAwait(false);
}