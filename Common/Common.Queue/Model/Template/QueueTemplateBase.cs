namespace Common.Queue.Model.Template;

/// <summary>
/// Template base
/// </summary>
public class QueueTemplateBase
{
    /// <summary>
    /// Identificador
    /// </summary>
    /// <returns></returns>
    public Guid InternalIdentifier { get; set; }

    /// <summary>
    /// RequestId
    /// </summary>
    public string RequestId { get; set; }
}