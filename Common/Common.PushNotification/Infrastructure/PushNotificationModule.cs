using Autofac;
using Common.PushNotification.Implementations.FirebaseHuawei;
using Common.PushNotification.Implementations.FirebaseHuawei.Firebase;
using Common.PushNotification.Implementations.FirebaseHuawei.Huawei;
using Common.PushNotification.Implementations.Indigitall;
namespace Common.PushNotification.Infrastructure;
public class PushNotificationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<FirebasePushNotification>().Keyed<IPushNotificationPlatform>(PushNotificationPlatformImplementationNames.Firebase.ToString().ToUpper());
        builder.RegisterType<HuaweiPushNotification>().Keyed<IPushNotificationPlatform>(PushNotificationPlatformImplementationNames.Huawei.ToString().ToUpper());
        
        builder.RegisterType<IndigitallPushNotification>().Keyed<IPushNotification>(PushNotificationImplementationNames.Indigitall.ToString().ToUpper());
        builder.RegisterType<FirebaseHuaweiPushNotification>().Keyed<IPushNotification>(PushNotificationImplementationNames.FirebaseHuawei.ToString().ToUpper());
    }
}
