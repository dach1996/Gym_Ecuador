using System.Text.Json.Serialization;
using Common.WebCommon.Models.WebSocketApi.Event;
using LogicCommon.Model.Common.Hub;
using LogicCommon.Model.Response;
namespace LogicCommon.Model.Request.Event;

/// <summary>
/// Request para notificar Evento
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="guidIdentifier"></param>
/// <param name="eventName"></param>
/// <param name="groupName"></param>
/// <param name="model"></param>
/// <param name="commonContextRequest"></param>
public class NotifyGroupEventRequest(
    EventHubName eventName,
    string groupName,
    IHubModel model,
    CommonContextRequest commonContextRequest
        ) : ICommonBaseRequest<GenericCommonOperationResponse>
{

    /// <summary>
    /// Nombre de Evento
    /// </summary>
    /// <value></value>
    public EventHubName EventName { get; private set; } = eventName;

    /// <summary>
    /// Nombre de Grupo
    /// </summary>
    /// <value></value>
    public string GroupName { get; private set; } = groupName;

    /// <summary>
    /// Data
    /// </summary>
    /// <value></value>
    public IHubModel Model { get; private set; } = model;

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest CommonContextRequest { get; set; } = commonContextRequest;
}