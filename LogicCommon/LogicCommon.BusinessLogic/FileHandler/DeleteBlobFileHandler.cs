using LogicCommon.Model.Request.File;
using LogicCommon.Model.Response;

namespace LogicCommon.BusinessLogic.FileHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class DeleteBlobFileHandler(
    ILogger<DeleteBlobFileHandler> logger,
    IPluginFactory pluginFactory) : FileBase<DeleteBlobFileRequest, GenericCommonOperationResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GenericCommonOperationResponse> Handle(DeleteBlobFileRequest request, CancellationToken cancellationToken)
    {
        await BlobBus.DeleteFileAsync(request.FileName, request.Path).ConfigureAwait(false);
        return GenericCommonOperationResponse.SuccessOperation();
    }
}