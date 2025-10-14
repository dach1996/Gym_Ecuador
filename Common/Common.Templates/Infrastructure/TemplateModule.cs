using Autofac;
using Common.Templates.Implementations;
using Common.Templates.Implementations.Types;
using Common.Templates.Interface;
using Common.Templates.Interface.Types;
namespace Common.Templates.Infrastructure;
public class TemplateModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TemplateFactory>().As<ITemplateFactory>().SingleInstance();
        builder.RegisterType<NotificationTemplate>().As<INotificationTemplate>().SingleInstance();
    }
}