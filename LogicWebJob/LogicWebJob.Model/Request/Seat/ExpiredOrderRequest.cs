using Common.WebJob.Model;
namespace LogicWebJob.Model.Request.Seat;
/// <summary>
/// Request para Expirar Orden
/// </summary>
public class ExpiredOrderRequest : IFunctionOperationRequest
{
    /// <summary>
    /// Id de Orden
    /// </summary>
    /// <value></value>
    public IEnumerable<long> OrderIds { get; set; }

    /// <summary>
    /// Contexto de la operación
    /// </summary>
    /// <value></value>
    public WebJobContext WebJobContext { get; set; }
}