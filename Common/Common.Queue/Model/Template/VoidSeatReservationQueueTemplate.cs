using Common.Queue.Model.Enum;

namespace Common.Queue.Model.Template;
/// <summary>
/// Modelo queue para cancelar reserva de asientos
/// </summary>
public class VoidSeatReservationQueueTemplate : IQueueTemplate
{
    /// <summary>
    /// Queue Name
    /// </summary>
    public QueueTemplateName QueueTemplateName => QueueTemplateName.VoidSeatReservation;

    /// <summary>
    /// Id de Orden
    /// </summary>
    /// <value></value>
    public long UserId { get; set; }

    /// <summary>
    /// Tickets a Excluir
    /// </summary>
    /// <value></value>
    public IEnumerable<Guid> ExcludeRouteGuids { get; set; }

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
    /// Constructor
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="excludeRouteGuids"></param>
    public VoidSeatReservationQueueTemplate(long userId, params Guid[] excludeRouteGuids)
    {
        UserId = userId;
        ExcludeRouteGuids = excludeRouteGuids;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public VoidSeatReservationQueueTemplate()
    {
    }
}
