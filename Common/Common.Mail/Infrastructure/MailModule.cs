using Autofac;
using Common.Mail.Implementation.Brevo;
using Common.Mail.Implementation.ElasticEmail;
using Common.Mail.Implementation.Mailgun;
using Common.Mail.Implementation.SendGrid;
using Common.Mail.Interface;
namespace Common.Mail.Infrastructure;
public class MailModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SendGridMailNotification>().Keyed<IMailNotification>($"{MailNotificationImplementationName.SendGrid.ToString().ToUpper()}");
        builder.RegisterType<MailgunMailNotification>().Keyed<IMailNotification>($"{MailNotificationImplementationName.Mailgun.ToString().ToUpper()}");
        builder.RegisterType<ElasticEmailMailNotification>().Keyed<IMailNotification>($"{MailNotificationImplementationName.ElasticEmail.ToString().ToUpper()}");
        builder.RegisterType<BrevoMailNotification>().Keyed<IMailNotification>($"{MailNotificationImplementationName.Brevo.ToString().ToUpper()}");
    }
}