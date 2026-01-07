using Common.Templates.Configurations.Types;
using Common.Templates.Interface.Types;
using Common.Templates.Models.Mail;
using Common.Templates.Models.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Common.Templates.Implementations.Types;
/// <summary>
/// Clase para Notificationes
/// </summary>
public class MailTemplate : TemplateBase, IMailTemplate
{
    protected override string Section => "MailNotificationConfiguration:Implementations";
    protected readonly IList<MailTemplateConfiguration> MailTemplateConfigurations;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    public MailTemplate(
        ILogger<MailTemplate> logger,
        IConfiguration configuration)
        : base(logger, configuration)
    {
        MailTemplateConfigurations = ConfigurationSection.Get<List<MailTemplateConfiguration>>();
    }

    public MailTemplateResponse GetTemplate(string mailImplementationIdentifier, IMailTemplateModel mailTemplateModel)
    {
        //Obtiene la implementación de Mail
        var mailImplementation = MailTemplateConfigurations.FirstOrDefault(first => first.Identifier == mailImplementationIdentifier)
            ?? throw new ArgumentException($"No se encuentra la implementación de Mail con Identificador: {mailImplementationIdentifier}", nameof(mailImplementationIdentifier));
        //Obtiene el template
        var template = mailImplementation?.Information?.Templates?.FirstOrDefault(first => first.Identifier == $"{mailTemplateModel.MailTemplateName}")
            ?? throw new InvalidOperationException($"No se encuentra el template con Identificador: {mailTemplateModel.MailTemplateName}");
        //Retorna respuesta
        return new(template.Code);
    }
}