using LogicApi.Model.Response.Order;
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
    public ContextRequest ContextRequest { get; set; }
}
