using Common.WebCommon.Models;
using Common.WebCommon.Models.Enum;
using LogicCommon.Abstraction.Interfaces.Notification;
using LogicCommon.Model.Common;
using LogicCommon.Model.Request.NotificationPush;

namespace LogicCommon.BusinessLogic.NotificationHandler.TopicNotificationImplementation;

/// <summary>
/// Implementación para tópico "Android"
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class AndroidTopicNotification(
    ILogger<AndroidTopicNotification> logger,
    IPluginFactory pluginFactory) : TopicNotificationBase(logger, pluginFactory), ITopicNotification
{
    /// <summary>
    /// Tipo de Tópico
    /// </summary>
    protected override TopicType TopicType => TopicType.Android;

    /// <summary>
    /// Obtiene todos los usuarios que aplican al Topico
    /// </summary>
    /// <returns></returns>
    public async Task<List<UserNotification>> GetUserNotificationsApplyTopicAsync(ICommonBaseRequest commonContextRequest)
        => await ExecuteHandlerAsync(commonContextRequest, async () =>
             await GetUserNotificationAsync(
                where => where.UserId != null
                && where.PushToken != null
                && where.Device.Platform == EnumsDataBase.PlatformType.Android
             ).ConfigureAwait(false)
        ).ConfigureAwait(false);

    /// <summary>
    /// Verifica si el usuario aplica al tópico
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<bool> VerifyApplyTopicAsync(VerifyApplyTopicRequest request)
        => Task.FromResult(request.Platform == Platform.Android);
}