using Common.WebJob.Model;

namespace LogicWebJob.Model.Request.Seat;
/// <summary>
/// Request para Expirar Orden
/// </summary>
public class VoidSeatReservationRequest : IFunctionOperationRequest
{
    /// <summary>
    /// Id de Orden
    /// </summary>
    /// <value></value>
    public long UserId { get; set; }

    /// <summary>
    /// Tickets a Excluir
    /// </summary>
    /// <value></value>
    public Guid[] ExcludeRouteGuids { get; set; }

    /// <summary>
    /// Contexto de la operación
    /// </summary>
    /// <value></value>
    public WebJobContext WebJobContext { get; set; }
}