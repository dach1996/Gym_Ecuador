namespace Common.EventHub.Models.Request;
/// <summary>
/// Request para para envío de Eventos por Grupo
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="groupName"></param>
/// <param name="eventName"></param>
/// <param name="dataModel"></param>
public class SendEventMessageByGroupRequest(
    string groupName,
    string eventName,
    object dataModel)
{
    /// <summary>
    /// Nombre de Evento
    /// </summary>
    /// <value></value>
    public string EventName { get; private set; } = eventName;

    /// <summary>
    /// Nombre de Grupo
    /// </summary>
    /// <value></value>
    public string GroupName { get; private set; } = groupName;

    /// <summary>
    /// Data
    /// </summary>
    /// <value></value>
    public dynamic DataModel { get; private set; } = dataModel;
}