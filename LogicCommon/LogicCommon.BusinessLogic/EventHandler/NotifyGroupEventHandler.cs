using Common.EventHub.Models.Request;
using Common.Utils.Extensions;
using LogicCommon.Model.Request.Event;
using LogicCommon.Model.Response;

namespace LogicCommon.BusinessLogic.EventHandler;

/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class NotifyGroupEventHandler(
    ILogger<NotifyGroupEventHandler> logger,
    IPluginFactory pluginFactory) : EventBase<NotifyGroupEventRequest, GenericCommonOperationResponse>(logger, pluginFactory)
{

    /// <summary>
    /// Manejador
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async override Task<GenericCommonOperationResponse> Handle(NotifyGroupEventRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(request, async () =>
        {
            await EventHub.SendMessageAsync(new SendEventMessageByGroupRequest(
                request.GroupName,
                request.EventName.GetEnumMember(),
                request.Model
            )).ConfigureAwait(false);
            return GenericCommonOperationResponse.SuccessOperation();
        }).ConfigureAwait(false);
}