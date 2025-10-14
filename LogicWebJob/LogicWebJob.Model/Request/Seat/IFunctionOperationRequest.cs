using Common.WebJob.Model;
using MediatR;

namespace LogicWebJob.Model.Request.Seat;
/// <summary>
/// Interface para las operaciones de la función
/// </summary>
public interface IFunctionOperationRequest : IRequest<Unit>
{
    /// <summary>
    /// Contexto de la operación
    /// </summary>
    WebJobContext WebJobContext { get; set; }
}
