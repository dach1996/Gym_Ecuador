using Common.Queue.Model.Enum;

namespace Common.Queue.Model.Template;

/// <summary>
/// Template base
/// </summary>
public interface IQueueTemplate
{
    /// <summary>
    /// Nombre del Queue
    /// </summary>
    /// <value></value>
    QueueTemplateName QueueTemplateName { get; }

    /// <summary>
    /// Identificador Externo
    /// </summary>
    /// <value></value>
    Guid InternalIdentifier { get; set; }

    /// <summary>
    /// RequestId
    /// </summary>
    /// <value></value>
    string RequestId { get; set; }
}