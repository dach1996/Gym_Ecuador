using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicApi.Model.Response.Seat;
namespace LogicApi.Model.Request.Seat;
/// <summary>
/// Request de reservar un asiento
/// </summary>
public class GetSeatAvailableRequest : IApiBaseRequest, IRequest<GetSeatAvailableResponse>
{
    /// <summary>
    /// Id de Ruta
    /// </summary>
    /// <value></value>
    [ValidateGuid]
    public Guid RouteGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}
