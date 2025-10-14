using Common.Queue.Model.Enum;

namespace Common.Queue.Model.Template;
/// <summary>
/// Modelo queue para expirar ordenes de compra
/// </summary>
public class ExpiredOrderQueueTemplate : IQueueTemplate
{
    /// <summary>
    /// Queue Name
    /// </summary>
    public QueueTemplateName QueueTemplateName => QueueTemplateName.ExpiredOrder;

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
    /// Ids de ordenes a expirar
    /// </summary>
    /// <value></value>
    public IEnumerable<long> OrderIds { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="orderIds"></param>
    public ExpiredOrderQueueTemplate(long orderIds) => OrderIds = [orderIds];

    /// <summary>
    /// Constructor
    /// </summary>
    public ExpiredOrderQueueTemplate()
    {

    }

}

