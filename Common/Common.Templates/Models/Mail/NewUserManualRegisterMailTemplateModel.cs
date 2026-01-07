namespace Common.Templates.Models.Mail;

/// <summary>
/// Modelo de Template de Mail para Nuevo Usuario Manual Registro
/// </summary>
public class NewUserManualRegisterMailTemplateModel : IMailTemplateModel
{
    /// <summary>
    /// Nombre de Template de Mail
    /// </summary>
    /// <value></value>
    public MailTemplateName MailTemplateName => MailTemplateName.NewUserManualRegister;

    /// <summary>
    /// Nombre de Usuario
    /// </summary>
    /// <value></value>
    public string Email { get; set; }

    /// <summary>
    /// Contraseña
    /// </summary>
    /// <value></value>
    public string Password { get; set; }

    /// <summary>
    /// Nombre de la persona
    /// </summary>
    /// <value></value>
    public string PersonName { get; set; }

    /// <summary>
    /// Email de soporte
    /// </summary>
    /// <value></value>
    public string SupportEmail { get; set; }

    /// <summary>
    /// Link de acceso al sistema
    /// </summary>
    /// <value></value>
    public string Link { get; set; }
}