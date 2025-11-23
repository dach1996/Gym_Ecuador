using Common.WebApi.Models.ContextRequestModel;
using Common.WebCommon.Attributes.CustomDataAnnotations;
using Common.WebCommon.Models;
using LogicApi.Model.Enum.Implementation;
using LogicApi.Model.Response;
using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Order.Payment;
/// <summary>
/// Request para pagar orden
/// </summary>
public abstract class PaymentOrderBaseRequest : IApiBaseRequest<HandlerResponse>
{
    /// <summary>
    /// Id de Orden
    /// </summary>
    /// <value></value>
    [Required]
    [ValidateGuid]
    public Guid OrderGuid { get; set; }

    /// <summary>
    /// Implementación
    /// </summary>
    /// <value></value>
    [Required]
    [ValidateEnum]
    public PaymentCardImplementation Implementation { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
