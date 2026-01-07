namespace Common.Templates.Models.Mail;
/// <summary>
/// Modelo de Template de Mail
/// </summary>
public interface IMailTemplateModel
{
    /// <summary>
    /// Nombre de Template de Mail
    /// </summary>
    /// <value></value>
    MailTemplateName MailTemplateName { get; }
}