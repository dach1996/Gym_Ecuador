using Autofac;
using Common.PushNotification.Configuration;
using Common.PushNotification.Configuration.FirebaseHuawei;
using Common.PushNotification.Configuration.Indigitall;
using Common.PushNotification.Implementations.FirebaseHuawei.Models;
using Common.PushNotification.Model;
using Common.PushNotification.Model.Request;
using Common.PushNotification.PushNotificationException;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static Common.PushNotification.Model.Request.NotificationByTokenRequest;
namespace Common.PushNotification.Implementations.FirebaseHuawei;

public class FirebaseHuaweiPushNotification : PushNotificationBase, IPushNotification
{
    protected override PushNotificationImplementationNames Implementation => PushNotificationImplementationNames.FirebaseHuawei;
    protected readonly ILifetimeScope LifetimeScope;
    protected readonly FirebaseHuaweiConfiguration Configuration;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="webHostEnvironment"></param>
    public FirebaseHuaweiPushNotification(
        ILogger<FirebaseHuaweiPushNotification> logger,
        IConfiguration configuration,
        ILifetimeScope lifetimeScope)
        : base(
            logger)
    {
        Configuration = configuration.GetSection(nameof(PushNotificationConfiguration)).Get<PushNotificationConfiguration<FirebaseHuaweiConfiguration>>()
           ?.Implementations?.FirstOrDefault(where => where.Identifier == Implementation)?.Information
            ?? throw new CustomPushNotificationException($"No se encontró la configuración de servicios de Documentación con identificador{nameof(IndigitallConfiguration)}");
        LifetimeScope = lifetimeScope;
    }

    /// <summary>
    /// Envìo de notificaciones push por tokens
    /// </summary>
    /// <param name="notificationTokens"></param>
    /// <returns></returns>
    public async Task<NotificationResponse> SendNotificationAsync(NotificationByTokenRequest notificationTokens)
    {
        //Items de notificaciòn
        var notificationItems = Enumerable.Empty<NotificationItem>().ToList();
        //Recorre la lista y verifica si existe tokens que tengan implementaciones push Firebase
        foreach (var token in notificationTokens.Tokens)
        {
            var implmenetation = token.Key switch
            {
                BrandNotificationType.AndroidIOS => PushNotificationPlatformImplementationNames.Firebase,
                BrandNotificationType.Huawei => PushNotificationPlatformImplementationNames.Huawei,
                _ => throw new CustomPushNotificationException($"No se encontró la implementación para el tipo de notificación: {token.Key}")
            };
            LifetimeScope.TryResolveKeyed<IPushNotificationPlatform>($"{implmenetation}".ToUpper(), out var implementationResult);
            var notificationItemsResult = (await implementationResult
                .SendNotificationAsync(new NotificationTokensPlatformRequest(notificationTokens.Title, notificationTokens.Body, token.Value)).ConfigureAwait(false))
            ?.NotificationItems;
            notificationItems.AddRange([.. notificationItemsResult]);
        }
        return new NotificationResponse(notificationItems);
    }

    /// <summary>
    /// Envìo de notificaciones push por Tópico
    /// </summary>
    /// <param name="notificationTopic"></param>
    /// <returns></returns>
    public async Task<NotificationResponse> SendNotificationAsync(NotificationByTopicRequest notificationTopic)
    {
        //Items de notificaciòn
        var notificationItems = Enumerable.Empty<NotificationItem>().ToList();
        //Recorre la lista
        foreach (var implementation in Configuration.Implementations.Where(implementation => implementation.Enable))
        {
            LifetimeScope.TryResolveKeyed<IPushNotificationPlatform>($"{implementation.Identifier}".ToUpper(), out var implementationResult);
            var notificationItemsResult = (await implementationResult.SendNotificationAsync(notificationTopic).ConfigureAwait(false))
            ?.NotificationItems;
            notificationItems.AddRange(notificationItemsResult.ToList());
        }
        return new NotificationResponse(notificationItems);
    }
}
