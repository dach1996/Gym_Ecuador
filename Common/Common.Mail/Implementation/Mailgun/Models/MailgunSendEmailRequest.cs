namespace Common.Mail.Implementation.Mailgun.Models;

/// <summary>
/// Modelo de request para envío de correo a través de Mailgun API
/// </summary>
public class MailgunSendEmailRequest
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
    /// Contenido HTML del correo (cuando se envía contenido directo)
    /// </summary>
    public string Html { get; set; }

    /// <summary>
    /// Contenido de texto plano del correo
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Nombre del template almacenado en Mailgun (cuando se usa template)
    /// </summary>
    public string Template { get; set; }

    /// <summary>
    /// Variables del template con prefijo v: (ej: v:variableName)
    /// </summary>
    public Dictionary<string, string> TemplateVariables { get; set; } = new();

    /// <summary>
    /// Archivos adjuntos
    /// </summary>
    public List<MailgunAttachment> Attachments { get; set; } = new();
}

/// <summary>
/// Modelo para archivos adjuntos de Mailgun
/// </summary>
public class MailgunAttachment
{
    /// <summary>
    /// Nombre del archivo
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Contenido del archivo en bytes
    /// </summary>
    public byte[] Content { get; set; }

    /// <summary>
    /// Tipo de contenido (MIME type)
    /// </summary>
    public string ContentType { get; set; }
}
