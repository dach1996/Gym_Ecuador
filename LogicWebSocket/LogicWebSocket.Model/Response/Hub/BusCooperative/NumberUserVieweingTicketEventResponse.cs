namespace LogicWebSocket.Model.Response.Hub.BusCooperative;
/// <summary>
/// Respuesta de evento de usuarios viendo tickets 
/// </summary>
public class NumberUserVieweingTicketEventResponse(int userNumber)
{
    /// <summary>
    /// Número de usuarios
    /// </summary>
    /// <value></value>
    public int UserNumber { get; set; } = userNumber;
}