using System.Net;
using Common.Mail.Interface;
using Common.Mail.MailException;
using Common.Mail.Model;
using Common.Mail.Model.Configuration;
using Common.Mail.Model.Configuration.ElasticEmail;
using ElasticEmail.Api;
using ElasticEmail.Client;
using ElasticEmail.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Common.Mail.Implementation.ElasticEmail;

/// <summary>
/// Implementación de notificación de correo usando Elastic Email
/// </summary>
public class ElasticEmailMailNotification : MailNotificationBase
{
    protected override MailNotificationImplementationName ImplementationName => MailNotificationImplementationName.ElasticEmail;
    protected readonly Configuration ElasticEmailConfiguration;
    protected readonly EmailsApi EmailsApi;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    public ElasticEmailMailNotification(
        ILogger<ElasticEmailMailNotification> logger,
        IConfiguration configuration) : base(logger, configuration)
    {
        var elasticEmailConfig = configuration.GetSection(nameof(MailNotificationConfiguration))
            .Get<MailNotificationConfiguration<ElasticEmailConfiguration>>()
            ?.Implementations?.FirstOrDefault(where => where.Identifier == $"{ImplementationName}")?.Information
            ?? throw new CustomMailException($"No se encontró la configuración de {nameof(IMailNotification)} con identificador: {ImplementationName}");

        // Configurar Elastic Email
        ElasticEmailConfiguration = new Configuration();
        ElasticEmailConfiguration.ApiKey.Add("X-ElasticEmail-ApiKey", elasticEmailConfig.ApiKey);

        // Crear instancia de EmailsApi
        EmailsApi = new EmailsApi(ElasticEmailConfiguration);
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
            // Preparar lista de destinatarios
            var toList = request.ToMails?.ToList() ?? new List<string>();
            
            // Preparar lista de BCC
            var bccList = new List<string>();
            
            if (request.ToMailsCco is not null && request.ToMailsCco.Any())
            {
                bccList.AddRange(request.ToMailsCco);
            }

            // Default BCCs
            if (DefaultConfiguration.DefaultBccs is not null && DefaultConfiguration.DefaultBccs.Any())
            {
                var defaultBccs = DefaultConfiguration.DefaultBccs.Where(where =>
                    request.ToMails == null || !request.ToMails.Any(any => any.Equals(where, StringComparison.InvariantCultureIgnoreCase)));
                bccList.AddRange(defaultBccs);
            }

            // Crear recipients
            var recipients = new TransactionalRecipient(to: toList);
            if (bccList.Any())
            {
                recipients.BCC = bccList;
            }

            // Crear contenido del correo
            var content = new EmailContent(from: DefaultConfiguration.DefaultFrom)
            {
                Subject = request.TemplateId
            };

            // Configurar contenido del correo
            if (request.TemplateData != null)
            {
                if (request.TemplateData is string templateContent)
                {
                    // Contenido HTML directo
                    content.Body = new List<BodyPart>
                    {
                        new BodyPart
                        {
                            ContentType = BodyContentType.HTML,
                            Charset = "utf-8",
                            Content = templateContent
                        }
                    };
                }
                else
                {
                    // Usar template de Elastic Email
                    content.TemplateName = request.TemplateId;
                    
                    // Agregar variables del template
                    content.Merge = new Dictionary<string, string>();
                    
                    // Convertir TemplateData a diccionario de strings
                    var templateDataJson = System.Text.Json.JsonSerializer.Serialize(request.TemplateData);
                    var templateDataDict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, System.Text.Json.JsonElement>>(templateDataJson);
                    
                    if (templateDataDict != null)
                    {
                        foreach (var kvp in templateDataDict)
                        {
                            var value = kvp.Value.ValueKind == System.Text.Json.JsonValueKind.String
                                ? kvp.Value.GetString()
                                : kvp.Value.GetRawText();
                            content.Merge[kvp.Key] = value ?? string.Empty;
                        }
                    }
                }
            }
            else
            {
                // Contenido de texto plano
                content.Body = new List<BodyPart>
                {
                    new BodyPart
                    {
                        ContentType = BodyContentType.PlainText,
                        Charset = "utf-8",
                        Content = request.TemplateId
                    }
                };
            }

            // Agregar adjuntos
            if (request.Attachments is not null && request.Attachments.Count != 0)
            {
                content.Attachments = new List<MessageAttachment>();
                
                foreach (var attachment in request.Attachments)
                {
                    var bytes = Convert.FromBase64String(attachment.Base64Attachment);
                    var messageAttachment = new MessageAttachment(
                        bytes, 
                        attachment.Name, 
                        attachment.ContentType, 
                        bytes.Length);
                    content.Attachments.Add(messageAttachment);
                }
            }

            // Crear el mensaje transaccional
            var emailMessage = new EmailTransactionalMessageData(recipients: recipients, content: content);

            // Enviar correo
            var response = await EmailsApi.EmailsTransactionalPostAsync(emailMessage).ConfigureAwait(false);
            
            Logger.LogInformation("Respuesta Elastic Email: '{@ElasticEmailResponse}' - TransactionID: '{@TransactionID}'", 
                response, response?.TransactionID);

            return response != null;
        }
        catch (ApiException ex)
        {
            Logger.LogError(ex, "Error en {@Class}({@Method}): {@Message} - Status Code: {@StatusCode}", 
                nameof(ElasticEmailMailNotification), nameof(SendMailAsync), ex.Message, ex.ErrorCode);
            return false;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error en {@Class}({@Method}): {@Message}", 
                nameof(ElasticEmailMailNotification), nameof(SendMailAsync), ex.Message);
            return false;
        }
    }
}

