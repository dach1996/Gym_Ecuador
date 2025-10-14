using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicApi.Model.Response;
namespace LogicApi.Model.Request.Order;
/// <summary>
/// Cancelación manual de Ordene
/// </summary>
public class ManualCancelOrderRequest : IApiBaseRequest<HandlerResponse>
{
    /// <summary>
    /// Id de Orden
    /// </summary>
    /// <value></value>
    [Required]
    [ValidateGuid]
    public Guid OrderGuid { get; set; }

    /// <summary>
    /// Motivo 
    /// </summary>
    /// <value></value>
    public string Reason { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

}
