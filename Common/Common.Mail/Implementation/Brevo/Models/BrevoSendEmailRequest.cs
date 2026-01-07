namespace Common.Mail.Implementation.Brevo.Models;

/// <summary>
/// Modelo de request para envío de correo a través de Brevo API
/// </summary>
public class BrevoSendEmailRequest
{
    /// <summary>
    /// Información del remitente
    /// </summary>
    public BrevoSender Sender { get; set; }

    /// <summary>
    /// Lista de destinatarios
    /// </summary>
    public List<BrevoRecipient> To { get; set; }

    /// <summary>
    /// Lista de destinatarios en copia oculta (BCC)
    /// </summary>
    public List<BrevoRecipient> Bcc { get; set; }

    /// <summary>
    /// Asunto del correo
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// ID del template almacenado en Brevo
    /// </summary>
    public int? TemplateId { get; set; }

    /// <summary>
    /// Parámetros del template (variables)
    /// </summary>
    public Dictionary<string, object> Params { get; set; }

    /// <summary>
    /// Contenido HTML del correo (cuando se envía contenido directo)
    /// </summary>
    public string HtmlContent { get; set; }

    /// <summary>
    /// Contenido de texto plano del correo
    /// </summary>
    public string TextContent { get; set; }

    /// <summary>
    /// Archivos adjuntos
    /// </summary>
    public List<BrevoAttachment> Attachment { get; set; }
}

/// <summary>
/// Modelo para el remitente de Brevo
/// </summary>
public class BrevoSender
{
    /// <summary>
    /// Nombre del remitente
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Email del remitente
    /// </summary>
    public string Email { get; set; }
}

/// <summary>
/// Modelo para destinatarios de Brevo
/// </summary>
public class BrevoRecipient
{
    /// <summary>
    /// Email del destinatario
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Nombre del destinatario (opcional)
    /// </summary>
    public string Name { get; set; }
}

/// <summary>
/// Modelo para archivos adjuntos de Brevo
/// </summary>
public class BrevoAttachment
{
    /// <summary>
    /// Contenido del archivo en base64
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Nombre del archivo
    /// </summary>
    public string Name { get; set; }
}

