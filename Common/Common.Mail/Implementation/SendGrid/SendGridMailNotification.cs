using System.Net;
using Common.Mail.Interface;
using Common.Mail.MailException;
using Common.Mail.Model;
using Common.Mail.Model.Configuration;
using Common.Mail.Model.Configuration.SendGrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
namespace Common.Mail.Implementation.SendGrid;
public class SendGridMailNotification : MailNotificationBase
{
    protected override MailNotificationImplementationName ImplementationName => MailNotificationImplementationName.SendGrid;
    protected readonly SendGridClient SendGridClient;
    protected readonly SendGridConfiguration SendGridConfiguration;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public SendGridMailNotification(ILogger<SendGridMailNotification> logger, IConfiguration configuration) : base(logger, configuration)
    {
        SendGridConfiguration = configuration.GetSection(nameof(MailNotificationConfiguration)).Get<MailNotificationConfiguration<SendGridConfiguration>>()
         ?.Implementations?.FirstOrDefault(where => where.Identifier == $"{ImplementationName}")?.Information
          ?? throw new CustomMailException($"No se encontró la configuración de {nameof(IMailNotification)} con identificador: {ImplementationName}");
        SendGridClient = new(SendGridConfiguration.ApiKey);
    }

    /// <summary>
    /// Envía correo
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public override async Task<bool> SendMailAsync(MailTemplateRequest request)
    {
        try
        {
            var newMessage = new SendGridMessage();
            if (request.ToMails is not null && request.ToMails.Any())
                newMessage.AddTos(request.ToMails.Select(m => new EmailAddress(m, m)).ToList());
            if (request.ToMailsCco is not null && request.ToMailsCco.Any())
                newMessage.AddBccs(request.ToMailsCco.Select(m => new EmailAddress(m, m)).ToList());
            if (DefaultConfiguration.DefaultBccs.Any())
            {
                var bccs = DefaultConfiguration.DefaultBccs.Where(where => !request.ToMails.Any(any => any.Equals(where, StringComparison.InvariantCultureIgnoreCase)));
                if (bccs.Any())
                    newMessage.AddBccs(bccs.Select(m => new EmailAddress(m, m)).ToList());
            }
            var template = SendGridConfiguration.Templates.FirstOrDefault(where => where.Identifier == request.TemplateIdentifier)
            ?? throw new CustomMailException($"No se encuentra configurado el Template con Identificador: {request.TemplateIdentifier}");
            newMessage.SetTemplateId(template.Code);
            newMessage.SetTemplateData(request.TemplateData);
            var from = string.IsNullOrEmpty(template.From) ? DefaultConfiguration.DefaultFrom : template.From;
            newMessage.SetFrom(new EmailAddress(from));

            if (request.Attachments is not null && request.Attachments.Count != 0)
            {
                newMessage.Attachments = [];
                foreach (var mailAttachment in request.Attachments)
                {
                    newMessage.Attachments.Add(new Attachment()
                    {
                        Content = mailAttachment.Base64Attachment,
                        Filename = mailAttachment.Name,
                        Type = mailAttachment.ContentType
                    });
                }
            }
            var response = await SendGridClient.SendEmailAsync(newMessage).ConfigureAwait(false);
            var messageResponse = await response.Body.ReadAsStringAsync().ConfigureAwait(false);
            Logger.LogInformation("Respuesta sendgrid: '{@SendGridMessage}' - Código. '{@Code}'", messageResponse, response.StatusCode);
            return response.StatusCode is HttpStatusCode.OK or HttpStatusCode.Accepted;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error en {@Class}({@Method}): {@Message}", nameof(SendGridMailNotification), nameof(SendMailAsync), ex.Message);
            return false;
        }
    }
}