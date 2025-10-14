namespace LogicWebSocket.Model.Response.Ticket;
/// <summary>
/// Respuesta del servicio de obtener un número de usuarios por grupo
/// </summary>
public class GetCountUserByGroupResponse(int userNumber)
{
    /// <summary>
    /// Número de usuarios
    /// </summary>
    /// <value></value>
    public int UserNumber { get; set; } = userNumber;
}