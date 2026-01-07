using Common.Templates.Models.Mail;
using LogicCommon.Model.Response.Mail;

namespace LogicCommon.Model.Request.Mail;
/// <summary>
/// Request de envío de Mail
/// </summary>
public class SendMailRequest : IRequest<SendMailResponse>
{
    /// <summary>
    /// Template
    /// </summary>
    /// <value></value>
    public IMailTemplateModel MailTemplateModel { get; set; }

    /// <summary>
    /// Correos a enviar 
    /// </summary>
    /// <value></value>
    public IEnumerable<string> To { get; set; }

    /// <summary>
    /// Correos Ocultos
    /// </summary>
    /// <value></value>
    public IEnumerable<string> ToCco { get; set; }
}

