using Common.Templates.Models.Mail;
using Common.Templates.Models.Types;

namespace Common.Templates.Interface.Types;
/// <summary>
/// Templates de Notificación
/// </summary>
public interface IMailTemplate
{
    /// <summary>
    /// Obtiene template de Mail
    /// </summary>
    /// <param name="mailImplementationIdentifier"></param>
    /// <param name="mailTemplateModel"></param>
    /// <returns></returns>
    MailTemplateResponse GetTemplate(string mailImplementationIdentifier, IMailTemplateModel mailTemplateModel);
}