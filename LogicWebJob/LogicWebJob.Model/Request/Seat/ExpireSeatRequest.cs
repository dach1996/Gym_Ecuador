using Common.WebJob.Model;

namespace LogicWebJob.Model.Request.Seat;
/// <summary>
/// Request para Expirar Asiento
/// </summary>
public class ExpireSeatRequest : IFunctionOperationRequest
{
    /// <summary>
    /// Id de Asiento
    /// </summary>
    /// <value></value>
    public IEnumerable<long> SeatIds { get; set; }

    /// <summary>
    /// Contexto de la operación
    /// </summary>
    /// <value></value>
    public WebJobContext WebJobContext { get; set; }
}