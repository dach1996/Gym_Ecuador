namespace Common.Mail.Implementation.Mailgun.Models;

/// <summary>
/// Modelo de request específico para envío de correo usando templates de Mailgun
/// </summary>
public class MailgunTemplateRequest
{
    /// <summary>
    /// Dirección de correo del remitente
    /// </summary>
    public string From { get; set; }

    /// <summary>
    /// Lista de direcciones de correo destinatarias
    /// </summary>
    public List<string> To { get; set; } = new();

    /// <summary>
    /// Lista de direcciones de correo en copia oculta (BCC)
    /// </summary>
    public List<string> Bcc { get; set; } = new();

    /// <summary>
    /// Asunto del correo
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Nombre del template almacenado en Mailgun
    /// </summary>
    public string Template { get; set; }

    /// <summary>
    /// Variables del template en formato JSON para el header h:X-Mailgun-Variables
    /// </summary>
    public Dictionary<string, object> TemplateVariables { get; set; } = new();

    /// <summary>
    /// Archivos adjuntos
    /// </summary>
    public List<MailgunAttachment> Attachments { get; set; } = new();
}

