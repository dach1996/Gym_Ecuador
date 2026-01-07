namespace Common.Templates.Models.Mail;
/// <summary>
/// Template para envìo de correos para olvidé mi contraseña
/// </summary>
public class ForgottenPasswordMailMailTemplateModel : IMailTemplateModel
{
    /// <summary>
    /// Queue Name
    /// </summary>
    public MailTemplateName MailTemplateName => MailTemplateName.ForgottenPasswordMail;

    /// <summary>
    /// Link de la política de privacidad
    /// </summary>
    /// <value></value>
    public string PolityLink { get; set; }

    /// <summary>
    /// Link
    /// </summary>
    /// <value></value>
    public string Link { get; set; }

    /// <summary>
    /// Contraseña
    /// </summary>
    /// <value></value>
    public string Password { get; set; }

    /// <summary>
    /// Nombre
    /// </summary>
    /// <value></value>
    public string FirstName { get; set; }
}