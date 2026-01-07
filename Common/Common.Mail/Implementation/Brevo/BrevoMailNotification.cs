using System.Net;
using System.Text;
using System.Text.Json;
using Common.Mail.Interface;
using Common.Mail.MailException;
using Common.Mail.Model;
using Common.Mail.Model.Configuration;
using Common.Mail.Model.Configuration.Brevo;
using Common.Mail.Implementation.Brevo.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Common.Mail.Implementation.Brevo;

/// <summary>
/// Implementación de notificación de correo usando Brevo
/// </summary>
public class BrevoMailNotification : MailNotificationBase
{
    protected override MailNotificationImplementationName ImplementationName => MailNotificationImplementationName.Brevo;
    protected readonly HttpClient BrevoClient;
    protected readonly BrevoConfiguration BrevoConfiguration;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    /// <param name="httpClientFactory"></param>
    public BrevoMailNotification(
        ILogger<BrevoMailNotification> logger,
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory) : base(logger, configuration)
    {
        BrevoConfiguration = configuration.GetSection(nameof(MailNotificationConfiguration))
            .Get<MailNotificationConfiguration<BrevoConfiguration>>()
            ?.Implementations?.FirstOrDefault(where => where.Identifier == $"{ImplementationName}")?.Information
            ?? throw new CustomMailException($"No se encontró la configuración de {nameof(IMailNotification)} con identificador: {ImplementationName}");

        BrevoClient = httpClientFactory.CreateClient($"{ImplementationName}");
        BrevoClient.BaseAddress = new Uri(BrevoConfiguration.BaseUrl);
        BrevoClient.DefaultRequestHeaders.Add("api-key", BrevoConfiguration.ApiKey);
        BrevoClient.DefaultRequestHeaders.Add("accept", "application/json");
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
            var endpoint = "/v3/smtp/email";
            var brevoRequest = BuildBrevoRequest(request);
            var jsonContent = JsonSerializer.Serialize(brevoRequest, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            Logger.LogInformation("Request Brevo: '{@BrevoRequest}'", jsonContent);
            var response = await BrevoClient.PostAsync(endpoint, content).ConfigureAwait(false);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Logger.LogInformation("Respuesta Brevo: '{@BrevoResponse}' - Código: '{@StatusCode}'",
                responseContent, response.StatusCode);

            return response.StatusCode is HttpStatusCode.OK or HttpStatusCode.Created or HttpStatusCode.Accepted;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error en {@Class}({@Method}): {@Message}",
                nameof(BrevoMailNotification), nameof(SendMailAsync), ex.Message);
            return false;
        }
    }

    /// <summary>
    /// Construye el request de Brevo a partir del MailTemplateRequest
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private BrevoSendEmailRequest BuildBrevoRequest(MailTemplateRequest request)
    {
        // Parsear el email del remitente (formato: "Name <email@example.com>" o "email@example.com")
        var (senderName, senderEmail) = ParseEmailAddress(DefaultConfiguration.DefaultFrom);

        var brevoRequest = new BrevoSendEmailRequest
        {
            Sender = new BrevoSender
            {
                Name = senderName,
                Email = senderEmail
            }
        };

        // To
        if (request.ToMails is not null && request.ToMails.Any())
        {
            brevoRequest.To = [];
            foreach (var toEmail in request.ToMails)
            {
                var (_, email) = ParseEmailAddress(toEmail);
                brevoRequest.To.Add(new BrevoRecipient
                {
                    Email = email,
                    Name = email
                });
            }
        }

        // BCC
        if (request.ToMailsCco is not null && request.ToMailsCco.Any())
        {
            brevoRequest.Bcc = [];
            foreach (var bccEmail in request.ToMailsCco)
            {
                var (_, email) = ParseEmailAddress(bccEmail);
                brevoRequest.Bcc.Add(new BrevoRecipient
                {
                    Email = email,
                    Name = email
                });
            }
        }

        // Default BCCs
        if (DefaultConfiguration.DefaultBccs is not null && DefaultConfiguration.DefaultBccs.Any())
        {
            var bccs = DefaultConfiguration.DefaultBccs.Where(where =>
                request.ToMails == null || !request.ToMails.Any(any => any.Equals(where, StringComparison.InvariantCultureIgnoreCase)));
            brevoRequest.Bcc ??= [];
            foreach (var bccEmail in bccs)
            {
                var (_, email) = ParseEmailAddress(bccEmail);
                brevoRequest.Bcc.Add(new BrevoRecipient
                {
                    Email = email,
                    Name = email
                });
            }
        }

        // Body - Brevo soporta templates almacenados o contenido directo
        if (request.TemplateData != null)
        {
            if (request.TemplateData is string templateContent)
            {
                // Contenido HTML directo
                brevoRequest.HtmlContent = templateContent;
            }
            else
            {
                // Usar template almacenado en Brevo
                // TemplateId debe ser un número para Brevo
                if (int.TryParse(request.TemplateId, out var templateId))
                {
                    brevoRequest.TemplateId = templateId;
                }
                else
                {
                    // Si TemplateId no es un número, usar como contenido HTML
                    brevoRequest.HtmlContent = request.TemplateId;
                }

                // Agregar variables del template
                var templateDataJson = JsonSerializer.Serialize(request.TemplateData);
                var templateDataDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(templateDataJson);
                if (templateDataDict != null)
                {
                    brevoRequest.Params ??= [];
                    foreach (var kvp in templateDataDict)
                    {
                        object value = kvp.Value.ValueKind switch
                        {
                            JsonValueKind.String => kvp.Value.GetString() ?? string.Empty,
                            JsonValueKind.Number => kvp.Value.GetDecimal(),
                            JsonValueKind.True => true,
                            JsonValueKind.False => false,
                            JsonValueKind.Null => null,
                            JsonValueKind.Array => kvp.Value.EnumerateArray().Select(e => e.GetRawText()).ToList(),
                            JsonValueKind.Object => JsonSerializer.Deserialize<object>(kvp.Value.GetRawText()),
                            _ => kvp.Value.GetRawText()
                        };
                        brevoRequest.Params[kvp.Key] = value;
                    }
                }
            }
        }
        else
        {
            // Contenido de texto plano
            brevoRequest.TextContent = request.TemplateId;
        }

        // Attachments
        if (request.Attachments is not null && request.Attachments.Count != 0)
        {
            brevoRequest.Attachment = [];
            foreach (var attachment in request.Attachments)
            {
                brevoRequest.Attachment.Add(new BrevoAttachment
                {
                    Content = attachment.Base64Attachment,
                    Name = attachment.Name
                });
            }
        }

        return brevoRequest;
    }

    /// <summary>
    /// Parsea una dirección de correo en formato "Name <email@example.com>" o "email@example.com"
    /// </summary>
    /// <param name="emailAddress"></param>
    /// <returns>Tupla con (nombre, email)</returns>
    private static (string name, string email) ParseEmailAddress(string emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress))
        {
            return (string.Empty, string.Empty);
        }

        // Formato: "Name <email@example.com>"
        if (emailAddress.Contains('<') && emailAddress.Contains('>'))
        {
            var startIndex = emailAddress.IndexOf('<');
            var endIndex = emailAddress.IndexOf('>');
            var name = emailAddress.Substring(0, startIndex).Trim().Trim('"', '\'');
            var email = emailAddress.Substring(startIndex + 1, endIndex - startIndex - 1).Trim();
            return (name, email);
        }

        // Formato simple: "email@example.com"
        return (string.Empty, emailAddress.Trim());
    }
}

