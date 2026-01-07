using Common.PushNotification.Configuration;
using Common.PushNotification.Configuration.FirebaseHuawei;
using Common.PushNotification.Implementations.FirebaseHuawei.Models;
using Common.PushNotification.Model;
using Common.PushNotification.Model.Enum;
using Common.PushNotification.Model.Request;
using Common.PushNotification.PushNotificationException;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Common.PushNotification.Implementations.FirebaseHuawei.Firebase;

internal class FirebasePushNotification : PushNotificationPlatformBase, IPushNotificationPlatform
{
    protected override PushNotificationPlatformImplementationNames Implementation => PushNotificationPlatformImplementationNames.Firebase;
    protected readonly PlatformNotificationPushImplementation PlatformNotificationPushImplementation;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="webHostEnvironment"></param>
    /// <returns></returns>
    public FirebasePushNotification(
        ILogger<FirebasePushNotification> logger,
        IConfiguration configuration
        )
        : base(
            logger,
            configuration)
    {
        var firebaseHuaweiConfiguration = configuration.GetSection(nameof(PushNotificationConfiguration)).Get<PushNotificationConfiguration<FirebaseHuaweiConfiguration>>()
           ?.Implementations?.FirstOrDefault(where => where.Identifier == PushNotificationImplementationNames.FirebaseHuawei)?.Information
            ?? throw new CustomPushNotificationException($"No se encontró la configuración de servicios de Documentación con identificador{nameof(FirebaseHuaweiConfiguration)}");
        PlatformNotificationPushImplementation = firebaseHuaweiConfiguration.Implementations?.FirstOrDefault(impl => impl.Identifier == Implementation)
            ?? throw new CustomPushNotificationException($"No se encontró la configuración de servicios de Documentación con identificador{nameof(Implementation)}");
        InitialConfiguration();
    }

    /// <summary>
    /// Envío de notificación individual
    /// </summary>
    /// <param name="notificationToken"></param>
    /// <returns></returns>
    public async Task<NotificationResponse> SendNotificationAsync(NotificationTokensPlatformRequest notificationToken)
    {
        try
        {
            // Verifica si algún token está vacío
            if (notificationToken.Tokens.Any(string.IsNullOrEmpty))
                throw new CustomPushNotificationException($"Existen tokens vacíos, por favor verifique la lista de Tokens a enviar.");
            notificationToken.Tokens = notificationToken.Tokens.Distinct();
            //Armar mensaje
            var message = new MulticastMessage()
            {
                Notification = new()
                {
                    Title = notificationToken.Title,
                    Body = notificationToken.Body
                }
            };
            //Agregar imagen
            if (!string.IsNullOrEmpty(notificationToken.ImageUrl))
                message.Notification.ImageUrl = notificationToken.ImageUrl;
            //Agregar url de redirección
            if (notificationToken.Action == ClickActionType.OpenUrl)
                message.Data = new Dictionary<string, string>
            {
                { "ClickAction", notificationToken.Action.ToString() },
                { "Link", notificationToken.RedirectUrl }
            };
            //Agregar token para notificación individual
            message.Tokens = [.. notificationToken.Tokens];
            var response = await FirebaseMessaging.DefaultInstance.SendEachForMulticastAsync(message).ConfigureAwait(false);
            //Crea lista de respuestas
            var tokensIndex = notificationToken.Tokens.ToArray();
            List<NotificationItem> notificationsItem = [];
            for (int i = 0; i < notificationToken.Tokens.Count(); i++)
            {
                var responseItem = response.Responses[i];
                notificationsItem.Add(responseItem.Exception is null ?
                    NotificationItem.Success(responseItem.MessageId, tokensIndex[i]) :
                    NotificationItem.Fail(tokensIndex[i], responseItem.Exception?.Message));
            }
            //Enviar notificación
            return new NotificationResponse(notificationsItem);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al enviar Notificación de implementación: '{@Implementation}' al Token: '{@Token}' - Mensage: '{@Message}' - InnerException: '{@InnerException}'", Implementation, string.Join(",", notificationToken.Tokens), ex.Message, ex.InnerException?.Message);
            throw new CustomPushNotificationException(ex.Message);
        }
    }

    /// <summary>
    /// Envio de notificación por Tópic
    /// </summary>
    /// <param name="notificationTopic"></param>
    /// <returns></returns>
    public async Task<NotificationResponse> SendNotificationAsync(NotificationByTopicRequest notificationTopic)
    {
        try
        {
            //Armar mensaje
            var message = new Message()
            {
                Notification = new()
                {
                    Title = notificationTopic.Title,
                    Body = notificationTopic.Body
                }
            };
            //Agregar imagen
            if (!string.IsNullOrEmpty(notificationTopic.ImageUrl))
                message.Notification.ImageUrl = notificationTopic.ImageUrl;
            //Agregar url de redirección
            if (notificationTopic.Action == ClickActionType.OpenUrl)
                message.Data = new Dictionary<string, string>
            {
                { "ClickAction", notificationTopic.Action.ToString() },
                { "Link", notificationTopic.RedirectUrl }
            };
            //Agregar token para notificación individual
            message.Topic = notificationTopic.Topic;
            var messageId = await FirebaseMessaging.DefaultInstance.SendAsync(message).ConfigureAwait(false);
            //Enviar notificación
            return new NotificationResponse(messageId, message.Topic);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al enviar Notificación de implementación: '{@Implementation}' al Topico: '{@Topic}' - Mensage: '{@Message}' - InnerException: '{@InnerException}'", Implementation, notificationTopic.Topic, ex.Message, ex.InnerException?.Message);
            throw new CustomPushNotificationException(ex.Message);
        }
    }


    /// <summary>
    /// Configuración Inicial
    /// </summary>
    protected void InitialConfiguration()
    {
        if (FirebaseApp.DefaultInstance is null)
        {
            var decodeString = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(PlatformNotificationPushImplementation.CredentialBase64));
            _ = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromJson(decodeString)
            });
        }

    }
}