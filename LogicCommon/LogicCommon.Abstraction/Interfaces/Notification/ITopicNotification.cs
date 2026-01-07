using Common.WebCommon.Models;
using LogicCommon.Model.Common;
using LogicCommon.Model.Request.NotificationPush;

namespace LogicCommon.Abstraction.Interfaces.Notification;

/// <summary>
/// Interfaz para verificar tokens
/// </summary>
public interface ITopicNotification
{
    /// <summary>
    /// Obtiene los Usuarios que aplican al tópico
    /// </summary>
    /// <returns></returns>
    Task<List<UserNotification>> GetUserNotificationsApplyTopicAsync(ICommonBaseRequest commonContextRequest);

    /// <summary>
    /// Verifica si determinados datos aplican al token
    /// </summary>
    /// <returns></returns>
    Task<bool> VerifyApplyTopicAsync(VerifyApplyTopicRequest request);
}