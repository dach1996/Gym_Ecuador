using Autofac;
namespace Common.Mail.Infrastructure;
public static class MailExtensions
{
    public static void UseMail(this ContainerBuilder builder) =>
        builder.RegisterModule<MailModule>();
}