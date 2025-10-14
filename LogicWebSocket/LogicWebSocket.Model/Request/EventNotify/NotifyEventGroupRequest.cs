namespace LogicWebSocket.Model.Request.EventNotify;
/// <summary>
/// Request para para envío de Eventos por Grupo
/// </summary>
public class NotifyEventGroupRequest
{
    /// <summary>
    /// Nombre de Evento
    /// </summary>
    /// <value></value>
    public string EventName { get; set; }

    /// <summary>
    /// Nombre de Grupo
    /// </summary>
    /// <value></value>
    public string GroupName { get; set; }
    /// <summary>
    ///  
    /// /// </summary>
    /// <value></value>
    public object DataModel { get; set; }
}
