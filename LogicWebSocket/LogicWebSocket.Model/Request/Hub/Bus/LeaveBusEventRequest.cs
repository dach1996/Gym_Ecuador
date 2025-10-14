namespace LogicWebSocket.Model.Request.Hub.Bus;
/// <summary>
/// Solicitud para dejar un evento de bus
/// </summary>
public class LeaveBusEventRequest
{
    /// <summary>
    /// Identificador de la ruta
    /// </summary>
    public Guid RouteGuid { get; set; }
}