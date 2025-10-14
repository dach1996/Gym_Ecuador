using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicApi.Model.Request.Order.Common;
using LogicApi.Model.Response.Order;
namespace LogicApi.Model.Request.Order;
/// <summary>
/// Request para confirmar la Orden
/// </summary>
public class VerifyOrderValuesRequest : IApiBaseRequest<VerifyOrderValuesResponse>
{
    /// <summary>
    /// Id de Cooperativa
    /// </summary>
    /// <value></value>
    [ValidateGuid]
    public Guid RouteGuid { get; set; }

    /// <summary>
    /// Asientas Personas
    /// </summary>
    /// <value></value>
    [Required]
    [MinLength(1)]
    public SeatPersonRequest[] SeatPeople { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}
