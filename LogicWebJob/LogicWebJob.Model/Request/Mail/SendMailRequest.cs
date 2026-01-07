using MediatR;

namespace LogicWebJob.Model.Request.Mail;
/// <summary>
/// Request de envío de Mail
/// </summary>
public class SendMailRequest : IRequest<Unit>
{
   

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