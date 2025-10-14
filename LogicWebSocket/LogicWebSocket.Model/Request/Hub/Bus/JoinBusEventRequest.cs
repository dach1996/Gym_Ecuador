using System.ComponentModel.DataAnnotations;

namespace LogicWebSocket.Model.Request.Hub.Bus;
/// <summary>
/// Solicitud para unirse a un evento de bus
/// </summary>
public class JoinBusEventRequest
{
    /// <summary>
    /// Identificador del ticket
    /// </summary>
    [Required]
    public Guid RouteGuid { get; set; }
}