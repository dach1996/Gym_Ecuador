using LogicAdministratorApi.Model.Request.NotificationPush;
using LogicAdministratorApi.Model.Response.NotificationPush;
using LogicCommon.Model.Request.NotificationPush;

namespace LogicAdministratorApi.BusinessLogic.NotificationPushHandler;

/// <summary>
/// Handler para enviar notificación push por UserGuids
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class SendNotificationPushByUserGuidsHandler(
    ILogger<SendNotificationPushByUserGuidsHandler> logger,
    IPluginFactory pluginFactory) : NotificationPushBase<SendNotificationPushByUserGuidsRequest, SendNotificationPushByUserGuidsResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja el envío de notificación push por UserGuids
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<SendNotificationPushByUserGuidsResponse> Handle(SendNotificationPushByUserGuidsRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.SendNotificationPushByUserGuids, request, async () =>
        {
            // Convierte UserGuids a UserIds
            var userIds = await ConvertUserGuidsToUserIdsAsync(request.UserGuids).ConfigureAwait(false);
            
            if (userIds.IsNullOrEmpty())
                throw new CustomException((int)MessagesCodesError.SystemError, "No se encontraron usuarios válidos con los UserGuids proporcionados.");

            // Crea el request para el handler de LogicCommon
            var sendNotificationRequest = new SendNotificationPushUsersRequest(
                request.Title,
                request.Body,
                userIds,
                request.ContextRequest)
            {
                ImageUrl = request.ImageUrl
            };

            // Envía la notificación usando el handler existente
            var notificationResponse = await Mediator.Send(sendNotificationRequest, cancellationToken).ConfigureAwait(false);

            // Obtiene el mapeo de UserId a UserGuid para la respuesta
            var userIdToGuidMap = await GetUserIdToGuidMapAsync(userIds).ConfigureAwait(false);

            // Convierte la respuesta a formato con UserGuids
            var responseByGuids = notificationResponse.NotificationPushUsers
                .ToDictionary(
                    kvp => userIdToGuidMap.GetValueOrDefault(kvp.Key, Guid.Empty),
                    kvp => new NotificationPushUserGuidResponse
                    {
                        TotalDevices = kvp.Value.TotalRegister,
                        SuccessDevices = kvp.Value.SuccessCount,
                        FailDevices = kvp.Value.FailCount,
                        SuccessOperation = kvp.Value.SuccessOperation
                    }
                )
                .Where(kvp => kvp.Key != Guid.Empty)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return new SendNotificationPushByUserGuidsResponse
            {
                NotificationPushUsers = responseByGuids,
                TotalUsers = responseByGuids.Count,
                SuccessUsers = responseByGuids.Count(kvp => kvp.Value.SuccessOperation),
                FailUsers = responseByGuids.Count(kvp => !kvp.Value.SuccessOperation),
                UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                ShowMessage = false
            };
        }).ConfigureAwait(false);
}

