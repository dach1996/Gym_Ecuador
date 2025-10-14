using Autofac;
using Common.Mail.Implementation.SendGrid;
using Common.Mail.Interface;
using Common.Mail.Model;
namespace Common.Mail.Infrastructure;
public class MailModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SendGridMailNotification>().Keyed<IMailNotification>($"{MailNotificationImplementationName.SendGrid.ToString().ToUpper()}");
    }
}