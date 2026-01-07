using System.Net;
using System.Text;
using System.Text.Json;
using Common.Mail.Interface;
using Common.Mail.MailException;
using Common.Mail.Model;
using Common.Mail.Model.Configuration;
using Common.Mail.Model.Configuration.Mailgun;
using Common.Mail.Implementation.Mailgun.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace Common.Mail.Implementation.Mailgun;

public class MailgunMailNotification : MailNotificationBase
{
    protected override MailNotificationImplementationName ImplementationName => MailNotificationImplementationName.Mailgun;
    protected readonly HttpClient MailgunClient;
    protected readonly MailgunConfiguration MailgunConfiguration;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    public MailgunMailNotification(
        ILogger<MailgunMailNotification> logger,
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory
        ): base(logger, configuration)
    {
        MailgunConfiguration = configuration.GetSection(nameof(MailNotificationConfiguration)).Get<MailNotificationConfiguration<MailgunConfiguration>>()
         ?.Implementations?.FirstOrDefault(where => where.Identifier == $"{ImplementationName}")?.Information
          ?? throw new CustomMailException($"No se encontró la configuración de {nameof(IMailNotification)} con identificador: {ImplementationName}");
        MailgunClient = httpClientFactory.CreateClient($"{ImplementationName}");
        MailgunClient.BaseAddress = new Uri(MailgunConfiguration.BaseUrl);
        MailgunClient.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"api:{MailgunConfiguration.ApiKey}"))}");
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
            var endpoint = $"/v3/{MailgunConfiguration.Domain}/messages";
            MultipartFormDataContent content;

            // Si TemplateData no es string y existe, usar el modelo de template con header h:X-Mailgun-Variables
            if (request.TemplateData != null && request.TemplateData is not string)
            {
                var templateRequest = BuildMailgunTemplateRequest(request);
                content = BuildMultipartFormDataForTemplate(templateRequest);
            }
            else
            {
                var mailgunRequest = BuildMailgunRequest(request);
                content = BuildMultipartFormData(mailgunRequest);
            }

            var response = await MailgunClient.PostAsync(endpoint, content).ConfigureAwait(false);
            
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Logger.LogInformation("Respuesta Mailgun: '{@MailgunResponse}' - Código: '{@StatusCode}'", 
                responseContent, response.StatusCode);

            return response.StatusCode is HttpStatusCode.OK or HttpStatusCode.Accepted;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error en {@Class}({@Method}): {@Message}", 
                nameof(MailgunMailNotification), nameof(SendMailAsync), ex.Message);
            return false;
        }
    }

    /// <summary>
    /// Construye el request de Mailgun a partir del MailTemplateRequest
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private MailgunSendEmailRequest BuildMailgunRequest(MailTemplateRequest request)
    {
        var mailgunRequest = new MailgunSendEmailRequest
        {
            From = DefaultConfiguration.DefaultFrom,
            Subject = request.TemplateId
        };

        // To
        if (request.ToMails is not null && request.ToMails.Any())
        {
            mailgunRequest.To.AddRange(request.ToMails);
        }

        // BCC
        if (request.ToMailsCco is not null && request.ToMailsCco.Any())
        {
            mailgunRequest.Bcc.AddRange(request.ToMailsCco);
        }

        // Default BCCs
        if (DefaultConfiguration.DefaultBccs is not null && DefaultConfiguration.DefaultBccs.Any())
        {
            var bccs = DefaultConfiguration.DefaultBccs.Where(where => 
                request.ToMails == null || !request.ToMails.Any(any => any.Equals(where, StringComparison.InvariantCultureIgnoreCase)));
            mailgunRequest.Bcc.AddRange(bccs);
        }

        // Body - Mailgun soporta templates almacenados o contenido directo
        // Si TemplateData es un string, usarlo como contenido HTML/texto
        // Si es un objeto, usar TemplateId como nombre de template de Mailgun y TemplateData como variables
        if (request.TemplateData != null)
        {
            if (request.TemplateData is string templateContent)
            {
                mailgunRequest.Html = templateContent;
            }
            else
            {
                // Usar template almacenado en Mailgun
                mailgunRequest.Template = request.TemplateId;
                
                // Agregar variables como v:variableName según la documentación de Mailgun
                var templateDataJson = JsonSerializer.Serialize(request.TemplateData);
                var templateDataDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(templateDataJson);
                if (templateDataDict != null)
                {
                    foreach (var kvp in templateDataDict)
                    {
                        var value = kvp.Value.ValueKind == JsonValueKind.String 
                            ? kvp.Value.GetString() 
                            : kvp.Value.GetRawText();
                        mailgunRequest.TemplateVariables[kvp.Key] = value ?? string.Empty;
                    }
                }
            }
        }
        else
        {
            mailgunRequest.Text = request.TemplateId;
        }

        // Attachments
        if (request.Attachments is not null && request.Attachments.Count != 0)
        {
            foreach (var attachment in request.Attachments)
            {
                var bytes = Convert.FromBase64String(attachment.Base64Attachment);
                mailgunRequest.Attachments.Add(new MailgunAttachment
                {
                    Name = attachment.Name,
                    Content = bytes,
                    ContentType = attachment.ContentType
                });
            }
        }

        return mailgunRequest;
    }

    /// <summary>
    /// Construye el request de template de Mailgun a partir del MailTemplateRequest
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private MailgunTemplateRequest BuildMailgunTemplateRequest(MailTemplateRequest request)
    {
        var templateRequest = new MailgunTemplateRequest
        {
            From = DefaultConfiguration.DefaultFrom,
            Subject = request.TemplateId,
            Template = request.TemplateId
        };

        // To
        if (request.ToMails is not null && request.ToMails.Any())
        {
            templateRequest.To.AddRange(request.ToMails);
        }

        // BCC
        if (request.ToMailsCco is not null && request.ToMailsCco.Any())
        {
            templateRequest.Bcc.AddRange(request.ToMailsCco);
        }

        // Default BCCs
        if (DefaultConfiguration.DefaultBccs is not null && DefaultConfiguration.DefaultBccs.Any())
        {
            var bccs = DefaultConfiguration.DefaultBccs.Where(where => 
                request.ToMails == null || !request.ToMails.Any(any => any.Equals(where, StringComparison.InvariantCultureIgnoreCase)));
            templateRequest.Bcc.AddRange(bccs);
        }

        // Template Variables - convertir TemplateData a diccionario
        if (request.TemplateData != null && request.TemplateData is not string)
        {
            var templateDataJson = JsonSerializer.Serialize(request.TemplateData);
            var templateDataDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(templateDataJson);
            if (templateDataDict != null)
            {
                foreach (var kvp in templateDataDict)
                {
                    object value = kvp.Value.ValueKind switch
                    {
                        JsonValueKind.String => kvp.Value.GetString() ?? string.Empty,
                        JsonValueKind.Number => kvp.Value.GetDecimal(),
                        JsonValueKind.True => true,
                        JsonValueKind.False => false,
                        JsonValueKind.Null => null,
                        _ => kvp.Value.GetRawText()
                    };
                    templateRequest.TemplateVariables[kvp.Key] = value;
                }
            }
        }

        // Attachments
        if (request.Attachments is not null && request.Attachments.Count != 0)
        {
            foreach (var attachment in request.Attachments)
            {
                var bytes = Convert.FromBase64String(attachment.Base64Attachment);
                templateRequest.Attachments.Add(new MailgunAttachment
                {
                    Name = attachment.Name,
                    Content = bytes,
                    ContentType = attachment.ContentType
                });
            }
        }

        return templateRequest;
    }

    /// <summary>
    /// Construye el contenido multipart/form-data para la petición HTTP
    /// </summary>
    /// <param name="mailgunRequest"></param>
    /// <returns></returns>
    private static MultipartFormDataContent BuildMultipartFormData(MailgunSendEmailRequest mailgunRequest)
    {
        var content = new MultipartFormDataContent();

        // From
        content.Add(new StringContent(mailgunRequest.From), "from");

        // To
        foreach (var to in mailgunRequest.To)
        {
            content.Add(new StringContent(to), "to");
        }

        // BCC
        foreach (var bcc in mailgunRequest.Bcc)
        {
            content.Add(new StringContent(bcc), "bcc");
        }

        // Subject
        content.Add(new StringContent(mailgunRequest.Subject), "subject");

        // Body
        if (!string.IsNullOrEmpty(mailgunRequest.Html))
        {
            content.Add(new StringContent(mailgunRequest.Html), "html");
        }
        else if (!string.IsNullOrEmpty(mailgunRequest.Text))
        {
            content.Add(new StringContent(mailgunRequest.Text), "text");
        }
        else if (!string.IsNullOrEmpty(mailgunRequest.Template))
        {
            content.Add(new StringContent(mailgunRequest.Template), "template");
            
            // Template variables con prefijo v:
            foreach (var variable in mailgunRequest.TemplateVariables)
            {
                content.Add(new StringContent(variable.Value), $"v:{variable.Key}");
            }
        }

        // Attachments
        foreach (var attachment in mailgunRequest.Attachments)
        {
            var attachmentContent = new ByteArrayContent(attachment.Content);
            attachmentContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(attachment.ContentType);
            content.Add(attachmentContent, "attachment", attachment.Name);
        }

        return content;
    }

    /// <summary>
    /// Construye el contenido multipart/form-data para templates usando el header h:X-Mailgun-Variables
    /// </summary>
    /// <param name="templateRequest"></param>
    /// <returns></returns>
    private static MultipartFormDataContent BuildMultipartFormDataForTemplate(MailgunTemplateRequest templateRequest)
    {
        var content = new MultipartFormDataContent();

        // From
        content.Add(new StringContent(templateRequest.From), "from");

        // To
        foreach (var to in templateRequest.To)
        {
            content.Add(new StringContent(to), "to");
        }

        // BCC
        foreach (var bcc in templateRequest.Bcc)
        {
            content.Add(new StringContent(bcc), "bcc");
        }

        // Subject
        content.Add(new StringContent(templateRequest.Subject), "subject");

        // Template
        content.Add(new StringContent(templateRequest.Template), "template");

        // Template Variables como header h:X-Mailgun-Variables en formato JSON
        if (templateRequest.TemplateVariables != null && templateRequest.TemplateVariables.Any())
        {
            var variablesJson = JsonSerializer.Serialize(templateRequest.TemplateVariables);
            content.Add(new StringContent(variablesJson), "h:X-Mailgun-Variables");
        }

        // Attachments
        foreach (var attachment in templateRequest.Attachments)
        {
            var attachmentContent = new ByteArrayContent(attachment.Content);
            attachmentContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(attachment.ContentType);
            content.Add(attachmentContent, "attachment", attachment.Name);
        }

        return content;
    }
}

