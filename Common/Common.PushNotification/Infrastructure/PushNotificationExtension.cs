using Autofac;
namespace Common.PushNotification.Infrastructure;
/// <summary>
/// Extensión push notificación
/// </summary>
public static class PushNotificationExtension
{
    public static void UsePushNotification(this ContainerBuilder builder) =>
        builder.RegisterModule<PushNotificationModule>();
}
