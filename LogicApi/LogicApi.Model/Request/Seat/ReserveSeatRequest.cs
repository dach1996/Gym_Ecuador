using Common.WebApi.Models.ContextRequestModel;
using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicApi.Model.Response.Seat;
using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Seat;
/// <summary>
/// Request de reservar un asiento
/// </summary>
public class ReserveSeatRequest : IApiBaseRequest<ReserveSeatResponse>
{
    /// <summary>
    /// Identificador de Piso
    /// </summary>
    /// <value></value>
    [ValidateGuid]
    public Guid FloorBusCooperativeGuid { get; set; }

    /// <summary>
    /// Identificador de Asiento
    /// </summary>
    /// <value></value>
    [Required]
    public string SeatIdentifier { get; set; }

    /// <summary>
    /// Identificador de Viaje
    /// </summary>
    /// <value></value>
    [ValidateGuid]
    public Guid CooperativeRouteGuid { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
