using Autofac;
using Common.PluginFactory.Extensions;
using LogicCommon.Abstraction.Interfaces.Notification;

namespace LogicCommon.Abstraction.Infrastructure;
public static class CommonAbstractionsExtension
{
    public static void UseLogicCommonAbstractionsAssemblies(this ContainerBuilder builder)
    {
        builder.ScanAssembliesFor<ITopicNotification>();
    }

}
