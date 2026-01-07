using Autofac;
using Common.Mail;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Mail.Infrastructure;
public static class MailExtensions
{
    public static void UseMail(this ContainerBuilder builder) =>
        builder.RegisterModule<MailModule>();

    /// <summary>
    /// Registra los HttpClients necesarios para las implementaciones de Mail
    /// </summary>
    /// <param name="services"></param>
    public static void AddMailHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient($"{MailNotificationImplementationName.Mailgun}");
        services.AddHttpClient($"{MailNotificationImplementationName.Brevo}");
    }
}