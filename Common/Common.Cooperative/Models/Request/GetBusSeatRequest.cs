namespace Common.Cooperative.Models.Request;
/// <summary>
/// Request obtener los viajes disponibles 
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="ticketIdentifier"></param>
public class GetBusSeatRequest(string ticketIdentifier)
{
    /// <summary>
    /// Identificador de Ticket
    /// </summary>
    /// <value></value>
    public string TicketIdentifier { get; set; } = ticketIdentifier;
}