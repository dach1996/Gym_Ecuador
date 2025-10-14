using Common.Mail.Interface;
using Common.Mail.Model;
using Common.Mail.Model.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Common.Mail.Implementation;

/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="configuration"></param>
public abstract class MailNotificationBase(
    ILogger<MailNotificationBase> logger,
    IConfiguration configuration) : IMailNotification
{
    protected abstract MailNotificationImplementationName ImplementationName { get; }

    protected readonly MailNotificationConfiguration DefaultConfiguration = configuration.GetSection(nameof(MailNotificationConfiguration)).Get<MailNotificationConfiguration>();
    protected readonly ILogger<MailNotificationBase> Logger = logger;
    protected readonly IConfiguration Configuration = configuration;

    public abstract Task<bool> SendMailAsync(MailTemplateRequest request);
}