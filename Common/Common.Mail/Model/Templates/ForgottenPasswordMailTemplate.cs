using Common.Mail.Model.Enum;

namespace Common.Mail.Model.Templates;
/// <summary>
/// Template
/// </summary>
public class ForgottenPasswordMailTemplate : IMailTemplateModel
{
    /// <summary>
    /// Código de Template
    /// </summary>
    public MailTemplateCodes Identifier => MailTemplateCodes.ForgottenPassword;

    /// <summary>
    /// Nombre de usuario
    /// </summary>
    /// <value></value>
    public string UserName { get; set; }

    /// <summary>
    /// Contraseña 
    /// </summary>
    /// <value></value>
    public string NewPassword { get; set; }
}