using Common.Mail.Model.Enum;

namespace Common.Mail.Model.Templates;
/// <summary>
/// Template
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="password"></param>
/// <param name="user"></param>
public class NewUserTemporalPassowordMailTemplate(string user, string password) : IMailTemplateModel
{

    /// <summary>
    /// Código de Template
    /// </summary>
    public MailTemplateCodes Identifier => MailTemplateCodes.NewUserTemporalPassoword;

    /// <summary>
    /// Contraseña 
    /// </summary>
    /// <value></value>
    public string Password { get; set; } = password;

    /// <summary>
    /// Usuario 
    /// </summary>
    /// <value></value>
    public string User { get; set; } = user;

}