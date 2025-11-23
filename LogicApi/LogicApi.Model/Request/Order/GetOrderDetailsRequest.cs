using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Order;
using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Order;
/// <summary>
/// Request obtener detalle de la órden 
/// </summary>
public class GetOrderDetailsRequest : IApiBaseRequest<GetOrderDetailsResponse>
{
    /// <summary>
    /// Id de Origen
    /// </summary>
    /// <value></value>
    [Required]
    [Range(1, long.MaxValue)]
    public long OrderId { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
