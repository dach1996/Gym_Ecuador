using AGConnectAdmin;
using AGConnectAdmin.Messaging;
using Common.PushNotification.Configuration;
using Common.PushNotification.Configuration.FirebaseHuawei;
using Common.PushNotification.Model;
using Common.PushNotification.PushNotificationException;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Common.PushNotification.Implementations.FirebaseHuawei.Huawei;

public class HuaweiPushNotification : PushNotificationPlatformBase, IPushNotificationPlatform
{
    protected override PushNotificationPlatformImplementationNames Implementation => PushNotificationPlatformImplementationNames.Huawei;
    protected readonly PlatformNotificationPushImplementation NotificationConfiguration;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="webHostEnvironment"></param>
    /// <returns></returns>
    public HuaweiPushNotification(
        ILogger<HuaweiPushNotification> logger,
        IConfiguration configuration
        )
        : base(
            logger,
            configuration)
    {
        var firebaseHuaweiConfiguration = configuration.GetSection(nameof(PushNotificationConfiguration)).Get<PushNotificationConfiguration<FirebaseHuaweiConfiguration>>()
           ?.Implementations?.FirstOrDefault(where => where.Identifier == PushNotificationImplementationNames.FirebaseHuawei)?.Information
            ?? throw new CustomPushNotificationException($"No se encontró la configuración de servicios de Documentación con identificador{nameof(FirebaseHuaweiConfiguration)}");
        NotificationConfiguration = firebaseHuaweiConfiguration.Implementations?.FirstOrDefault(impl => impl.Identifier == Implementation)
            ?? throw new CustomPushNotificationException($"No se encontró la configuración de servicios de Documentación con identificador{nameof(Implementation)}");
    }

    /// <summary>
    /// Envío de notificación a Token
    /// </summary>
    /// <param name="notificationToken"></param>
    /// <returns></returns>
    /// <exception cref="CustomPushNotificationException"></exception>
    public async Task<NotificationResponse> SendNotificationAsync(NotificationTokensPlatform notificationToken)
    {
        try
        {
            List<NotificationItem> notificationsItem = new();
            //Verifica si está encendido
            if (!NotificationConfiguration.Enable)
                notificationsItem = notificationToken.Tokens.Select(select => NotificationItem.Fail(select, DISABLED_MESSAGE)).ToList();
            else
            {
                //Armar mensaje
                var message = BuildMessageHuiawei(notificationToken);
                //Agregar token para notificación individual
                message.Token = new List<string>(notificationToken.Tokens);
                //Obtener instancia de huawei
                var messageId = await AGConnectMessaging.DefaultInstance.SendAsync(message).ConfigureAwait(false);
                //Crea la lista de respuestas
                var tokensIndex = notificationToken.Tokens.ToArray();
                for (int i = 0; i < notificationToken.Tokens.Count(); i++)
                    notificationsItem.Add(!string.IsNullOrEmpty(messageId) ?
                        NotificationItem.Success(messageId, tokensIndex[i]) :
                        NotificationItem.Fail(tokensIndex[i], messageId));
            }
            //Enviar notificación
            return new NotificationResponse(notificationsItem);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al enviar Notificación de implementación: '{@Implementation}' al Token: '{@Topic}' - '{@Message}'", Implementation, string.Join(",", notificationToken.Tokens), ex.Message);
            throw new CustomPushNotificationException(ex.Message);
        }
    }


    /// <summary>
    /// Envío de notificación a Tópico
    /// </summary>
    /// <param name="notificationTopic"></param>
    /// <returns></returns>
    /// <exception cref="CustomPushNotificationException"></exception>
    public async Task<NotificationResponse> SendNotificationAsync(NotificationTopic notificationTopic)
    {
        try
        {
            NotificationResponse notificationResponse = null;
            if (!NotificationConfiguration.Enable)
                notificationResponse = new NotificationResponse(DISABLED_MESSAGE, notificationTopic.Topic, false);
            else
            {//Armar mensaje
                var message = BuildMessageHuiawei(notificationTopic);
                //Agregar token para notificación individual
                message.Topic = notificationTopic.Topic;
                //Obtener instancia de huawei
                var messageId = await AGConnectMessaging.DefaultInstance.SendAsync(message).ConfigureAwait(false);
                notificationResponse = new NotificationResponse(messageId, message.Topic);
            }
            //Enviar notificación
            return notificationResponse;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al enviar Notificación de implementación: '{@Implementation}' al Topico: '{@Topic}' - '{@Message}'", Implementation, notificationTopic.Topic, ex.Message);
            throw new CustomPushNotificationException(ex.Message);
        }
    }

    /// <summary>
    /// Armar request para envío de notificación de huawei
    /// </summary>
    /// <param name="NotificationModel"></param>
    /// <returns></returns>
    private static Message BuildMessageHuiawei(NotificationModelBase NotificationModel)
    {
        //Armar request de huawei
        var message = new Message()
        {
            Notification = new()
            {
                Title = NotificationModel.Title,
                Body = NotificationModel.Body
            },
            Android = new()
            {
                Notification = new()
                {
                    Title = NotificationModel.Title,
                    Body = NotificationModel.Body
                }
            }
        };
        //Agregar imagen
        if (!string.IsNullOrEmpty(NotificationModel.ImageUrl))
            message.Notification.Image = NotificationModel.ImageUrl;
        //Agregar url de redirección
        message.Android.Notification.ClickAction = NotificationModel.Action switch
        {
            NotificationAction.OpenApp => ClickAction.OpenApp(),
            NotificationAction.OpenUrl => ClickAction.OpenUrl(NotificationModel.RedirectUrl),
            _ => throw new NotImplementedException()
        };
        return message;
    }

    protected void InitialConfiguration()
    {
        var decodeString = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(NotificationConfiguration.CredentialBase64));
        _ = AGConnectApp.Create(new AppOptions()
        {
            LoginUri = string.Empty,
            ApiBaseUri = string.Empty,
            ClientId = decodeString,
            ClientSecret = string.Empty
        });
    }
}
