
namespace Common.Mail.Model;
/// <summary>
/// Clase base para envío de Mail
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="toMails"></param>
/// <param name="toMailsCco"></param>
/// <param name="attachments"></param>
public abstract class MailBaseRequest(
    IList<string> toMails,
    IList<string> toMailsCco = null,
    List<MailAttachment> attachments = null)
{

    /// <summary>
    /// Mails destino
    /// </summary>
    /// <value></value>
    public IList<string> ToMails { get; private set; } = toMails;

    /// <summary>
    /// Mails destino copia oculta
    /// </summary>
    /// <value></value>
    public IList<string> ToMailsCco { get; private set; } = toMailsCco;

    /// <summary>
    /// Archivos
    /// </summary>
    /// <value></value>
    public List<MailAttachment> Attachments { get; private set; } = attachments;
}

/// <summary>
/// Clase para envío de Mail
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="templateIdentifier"></param>
/// <param name="templateData"></param>
/// <param name="to"></param>
public class MailTemplateRequest(
    string templateId,
    object templateData,
    IEnumerable<string> to) : MailBaseRequest([.. to], null, null)
{
    /// <summary>
    /// Id de template
    /// </summary>
    /// <value></value>
    public string TemplateId { get; private set; } = templateId;
    
    /// <summary>
    /// Implementaciones de Template
    /// </summary>
    /// <value></value>
    public object TemplateData { get; private set; } = templateData;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="templateIdentifier"></param>
    /// <param name="templateData"></param>
    /// <param name="to"></param>
    public MailTemplateRequest(
        string templateIdentifier,
        object templateData,
        string to)
        : this(templateIdentifier, templateData, [to])
    {
    }
}
