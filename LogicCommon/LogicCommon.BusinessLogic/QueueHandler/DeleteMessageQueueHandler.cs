using Common.Utils.Extensions;
using LogicCommon.Model.Request.Queue;
using LogicCommon.Model.Response.Queue;
using QueueModel = Common.Queue.Model.Request;
namespace LogicCommon.BusinessLogic.QueueHandler;

/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class DeleteMessageQueueHandler(
    ILogger<DeleteMessageQueueHandler> logger,
    IPluginFactory pluginFactory) : QueueBase<DeleteMessageQueueRequest, DeleteMessageQueueResponse>(logger, pluginFactory)
{

    /// <summary>
    /// Manejador
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async override Task<DeleteMessageQueueResponse> Handle(DeleteMessageQueueRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(request, async () =>
        {
            var response = await Queue.DeleteMessageAsync(
                new QueueModel.DeleteQueueMessageRequest(
                    request.DeleteMessageQueueItems.Select(select =>
                    new QueueModel.DeleteQueueMessageItem(select.QueueTemplateName.GetEnumMember(), select.MessageId, select.PopReceipt))
                )
            ).ConfigureAwait(false);
            return DeleteMessageQueueResponse.SuccessResponse(response.Success, response.Total);
        }).ConfigureAwait(false);
}