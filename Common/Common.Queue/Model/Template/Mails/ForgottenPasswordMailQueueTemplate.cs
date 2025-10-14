using Common.Queue.Model.Enum;
using Common.Queue.Model.Template.Mails;

namespace Common.Queue.Model.Template;
/// <summary>
/// Template para envìo de correos 
/// </summary>
public class ForgottenPasswordMailQueueTemplate : QueueMailBase, IQueueTemplate
{
    /// <summary>
    /// Queue Name
    /// </summary>
    public QueueTemplateName QueueTemplateName => QueueTemplateName.ForgottenPasswordMail;

    /// <summary>
    /// Identificador Externo
    /// </summary>
    /// <value></value>
    public Guid InternalIdentifier { get; set; }

    /// <summary>
    /// RequestId
    /// </summary>
    /// <value></value>
    public string RequestId { get; set; }

    /// <summary>
    /// Nombre
    /// </summary>
    /// <value></value>
    public string UserName { get; set; }

    /// <summary>
    /// Nombre
    /// </summary>
    /// <value></value>
    public string NewPassword { get; set; }
}