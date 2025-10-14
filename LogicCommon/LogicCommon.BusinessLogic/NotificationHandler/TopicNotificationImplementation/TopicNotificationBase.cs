using System.Linq.Expressions;
using Common.WebCommon.Models.Enum;
using LogicCommon.Model.Common;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Models.Enums;
namespace LogicCommon.BusinessLogic.NotificationHandler.TopicNotificationImplementation;
/// <summary>
/// Clase base para implementaciones de topicos
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
/// <returns></returns>
public abstract class TopicNotificationBase(
    ILogger<TopicNotificationBase> logger,
    IPluginFactory pluginFactory) : BusinessLogicCommonBase(logger, pluginFactory)
{

    /// <summary>
    /// Tipo de Tópico
    /// </summary>
    /// <value></value>
    protected abstract TopicType TopicType { get; }

    /// <summary>
    /// Obtiene los usuarios a notificar
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    protected async Task<IEnumerable<UserNotification>> GetUserNotificationAsync(Expression<Func<UserDevicePushToken, bool>> where = null)
    {
        //Obtiene la información del los usuarios y dispositivos
        return await AuthenticationUnitOfWork.UserDevicePushTokenRepository
            .GetGenericAsync(
                select => new UserNotification
                {
                    UserId = select.UserId.Value,
                    DeviceId = select.DeviceId,
                    Implementation = GetImplementation(select.NotificationPushImplementationType)
                },
                where
        ).ConfigureAwait(false);
    }

    /// <summary>
    /// Obtiene la implementación a usar
    /// </summary>
    /// <param name="notificationPushImplementationType"></param>
    /// <returns></returns>
    private static string GetImplementation(NotificationPushImplementationType notificationPushImplementationType)
     => notificationPushImplementationType switch
     {
         NotificationPushImplementationType.Firebase => $"{NotificationPushImplementationType.Firebase}",
         NotificationPushImplementationType.Huawei => $"{NotificationPushImplementationType.Huawei}",
         _ => throw new NotImplementedException($"No se encuentra implementación para :{notificationPushImplementationType}"),
     };
}