using Autofac;
using Common.PushNotification.Configuration;
using Common.PushNotification.Configuration.FirebaseHuawei;
using Common.PushNotification.Model;
using Common.PushNotification.PushNotificationException;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
            ?? throw new CustomPushNotificationException($"No se encontró la configuración de servicios de Documentación con identificador{nameof(FirebaseHuaweiConfiguration)}");
        LifetimeScope = lifetimeScope;
    }

    /// <summary>
    /// Envìo de notificaciones push por tokens
    /// </summary>
    /// <param name="notificationTokens"></param>
    /// <returns></returns>
    public async Task<NotificationResponse> SendNotificationAsync(NotificationTokens notificationTokens)
    {
        var implementationTokens = notificationTokens.Tokens.GroupBy(t => t.Implementation)
            .Select(select => new
            {
                Implementation = select.Key,
                Tokens = select.Select(h => h.Token)
            });
        //Items de notificaciòn
        var notificationItems = Enumerable.Empty<NotificationItem>().ToList();
        //Recorre la lista y verifica si existe tokens que tengan implementaciones push Firebase
        var firebase = implementationTokens
            .FirstOrDefault(where => where.Implementation == PushNotificationPlatformImplementationNames.Firebase.ToString());
        if (firebase is not null)
        {
            LifetimeScope.TryResolveKeyed<IPushNotificationPlatform>($"{PushNotificationPlatformImplementationNames.Firebase}".ToUpper(), out var implementationResult);
            var notificationItemsResult = (await implementationResult
                .SendNotificationAsync(new NotificationTokensPlatform(
                    notificationTokens.Title,
                    notificationTokens.Body,
                    firebase.Tokens,
                    notificationTokens.ImageUrl,
                    notificationTokens.RedirectUrl,
                    notificationTokens.Action,
                    notificationTokens.Data
                    )).ConfigureAwait(false))
            ?.NotificationItems;
            notificationItems.AddRange([.. notificationItemsResult]);
        }
        //Recorre la lista y verifica si existe tokens que tengan implementaciones push Huawei
        var huawei = implementationTokens
            .FirstOrDefault(where => where.Implementation == PushNotificationPlatformImplementationNames.Huawei.ToString());
        if (huawei is not null)
        {
            LifetimeScope.TryResolveKeyed<IPushNotificationPlatform>($"{PushNotificationPlatformImplementationNames.Huawei}".ToUpper(), out var implementationResult);
            var notificationItemsResult = (await implementationResult
                .SendNotificationAsync(new NotificationTokensPlatform(
                    notificationTokens.Title,
                    notificationTokens.Body,
                    huawei.Tokens,
                    notificationTokens.ImageUrl,
                    notificationTokens.RedirectUrl,
                    notificationTokens.Action,
                    notificationTokens.Data)).ConfigureAwait(false))
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
    public async Task<NotificationResponse> SendNotificationAsync(NotificationTopic notificationTopic)
    {
        //Items de notificaciòn
        var notificationItems = Enumerable.Empty<NotificationItem>().ToList();
        //Recorre la lista
        foreach (var implementation in Configuration.Implementations.Where(implementation => implementation.Enable))
        {
            LifetimeScope.TryResolveKeyed<IPushNotificationPlatform>($"{implementation.Identifier}".ToUpper(), out var implementationResult);
            var notificationItemsResult = (await implementationResult.SendNotificationAsync(notificationTopic).ConfigureAwait(false))
            ?.NotificationItems;
            notificationItems.AddRange([.. notificationItemsResult]);
        }
        return new NotificationResponse(notificationItems);
    }
}
