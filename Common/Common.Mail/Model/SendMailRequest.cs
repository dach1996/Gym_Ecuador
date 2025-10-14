
namespace Common.Mail.Model;
/// <summary>
/// Clase base para envío de Mail
/// </summary>
public abstract class MailBaseRequest
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="toMails"></param>
    /// <param name="toMailsCco"></param>
    /// <param name="attachments"></param>
    protected MailBaseRequest(
        IList<string> toMails,
        IList<string> toMailsCco = null,
        List<MailAttachment> attachments = null)
    {
        ToMails = toMails;
        ToMailsCco = toMailsCco;
        Attachments = attachments;
    }

    /// <summary>
    /// Mails destino
    /// </summary>
    /// <value></value>
    public IList<string> ToMails { get; private set; }

    /// <summary>
    /// Mails destino copia oculta
    /// </summary>
    /// <value></value>
    public IList<string> ToMailsCco { get; private set; }

    /// <summary>
    /// Archivos
    /// </summary>
    /// <value></value>
    public List<MailAttachment> Attachments { get; private set; }
}

/// <summary>
/// Clase para envío de Mail
/// </summary>
public class MailTemplateRequest : MailBaseRequest
{
    /// <summary>
    /// Id de template
    /// </summary>
    /// <value></value>
    public string TemplateIdentifier { get; private set; }

    /// <summary>
    /// Implementaciones de Template
    /// </summary>
    /// <value></value>
    public object TemplateData { get; private set; }

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
        : this(templateIdentifier, templateData, new[] { to })
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="templateIdentifier"></param>
    /// <param name="templateData"></param>
    /// <param name="to"></param>
    public MailTemplateRequest(
        string templateIdentifier,
        object templateData,
        IEnumerable<string> to) : base(to.ToList(), null, null)
    {
        TemplateIdentifier = templateIdentifier;
        TemplateData = templateData;
    }

}
