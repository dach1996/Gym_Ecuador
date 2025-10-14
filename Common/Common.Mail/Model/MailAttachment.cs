namespace Common.Mail.Model;
public class MailAttachment
{
    /// <summary>
    /// Attachment in bas364
    /// </summary>
    public string Base64Attachment { get; set; }

    /// <summary>
    /// Attachment name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Attachment content-type
    /// </summary>
    public string ContentType { get; set; }
}