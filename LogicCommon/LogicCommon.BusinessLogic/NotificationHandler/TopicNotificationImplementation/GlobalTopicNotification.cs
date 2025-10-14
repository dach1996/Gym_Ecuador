using Common.WebCommon.Models;
using Common.WebCommon.Models.Enum;
using LogicCommon.Abstraction.Interfaces.Notification;
using LogicCommon.Model.Common;
using LogicCommon.Model.Request.NotificationPush;

namespace LogicCommon.BusinessLogic.NotificationHandler.TopicNotificationImplementation;
/// <summary>
/// Implementación para tópico "Global"
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GlobalTopicNotification(
    ILogger<GlobalTopicNotification> logger,
    IPluginFactory pluginFactory) : TopicNotificationBase(logger, pluginFactory), ITopicNotification
{
    /// <summary>
    /// Tipo de Tópico
    /// </summary>
    protected override TopicType TopicType => TopicType.Global;

    /// <summary>
    /// Obtiene todos los usuarios que aplican al Topico
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<UserNotification>> GetUserNotificationsApplyTopicAsync(ICommonBaseRequest commonContextRequest)
        => await ExecuteHandlerAsync(commonContextRequest, async () =>
             await GetUserNotificationAsync(
                where => where.UserId != null && where.PushToken != null
             ).ConfigureAwait(false)
        ).ConfigureAwait(false);



    /// <summary>
    /// Verifica si el usuario aplica al tópico
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<bool> VerifyApplyTopicAsync(VerifyApplyTopicRequest request)
        => Task.FromResult(true);
}