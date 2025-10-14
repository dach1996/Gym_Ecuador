using Common.Mail.Model.Templates;
using MediatR;

namespace LogicWebJob.Model.Request.Mail;
/// <summary>
/// Request de envío de Mail
/// </summary>
public class SendMailRequest : IRequest<Unit>
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