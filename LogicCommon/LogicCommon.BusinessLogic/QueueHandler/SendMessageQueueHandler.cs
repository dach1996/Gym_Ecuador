using Common.Utils.Extensions;
using LogicCommon.Model.Request.Queue;
using LogicCommon.Model.Response.Queue;
using Microsoft.AspNetCore.Http;

namespace LogicCommon.BusinessLogic.QueueHandler;

/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class SendMessageQueueHandler(
    ILogger<SendMessageQueueHandler> logger,
    IHttpContextAccessor httpContextAccessor,
    IPluginFactory pluginFactory) : QueueBase<SendMessageQueueRequest, SendMessageQueueResponse>(logger, pluginFactory)
{

    /// <summary>
    /// Manejador
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async override Task<SendMessageQueueResponse> Handle(SendMessageQueueRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(request, async () =>
        {
            request.QueueTemplate.InternalIdentifier = Guid.NewGuid();
            request.QueueTemplate.RequestId = httpContextAccessor.HttpContext?.TraceIdentifier ?? string.Empty;
            var response = await Queue.SendMessageAsync(new(request.QueueTemplate.QueueTemplateName.GetEnumMember(), request.QueueTemplate.ToJson(), request.DelaySeconds)).ConfigureAwait(false);
            return new SendMessageQueueResponse(
                response.Success,
                response.Message,
                response.MessageId,
                response.PopReceipt,
                request.QueueTemplate.InternalIdentifier,
                (byte)request.QueueTemplate.QueueTemplateName);
        }).ConfigureAwait(false);
}