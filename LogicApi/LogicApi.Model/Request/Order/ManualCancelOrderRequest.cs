using Common.WebApi.Models.ContextRequestModel;
using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicApi.Model.Response;
using Common.WebCommon.Models;
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
    public CommonContextRequest ContextRequest { get; set; }

}
