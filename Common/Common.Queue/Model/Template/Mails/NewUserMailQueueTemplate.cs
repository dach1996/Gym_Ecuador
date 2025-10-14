using Common.Queue.Model.Enum;
using Common.Queue.Model.Template.Mails;

namespace Common.Queue.Model.Template;
/// <summary>
/// Template para envìo de correos de Nuevo Usuario
/// </summary>
public class NewUserMailQueueTemplate : QueueMailBase, IQueueTemplate
{
    /// <summary>
    /// Queue Name
    /// </summary>
    public QueueTemplateName QueueTemplateName => QueueTemplateName.NewUserMail;

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
    /// Contraseña 
    /// </summary>
    /// <value></value>
    public string Password { get; set; }

    /// <summary>
    /// Usuario 
    /// </summary>
    /// <value></value>
    public string User { get; set; }
}