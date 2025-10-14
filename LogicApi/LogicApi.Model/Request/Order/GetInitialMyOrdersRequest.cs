using LogicApi.Model.Response.Order;
namespace LogicApi.Model.Request.Order;
/// <summary>
/// Request para generar Orden
/// </summary>
public class GetInitialMyOrdersRequest : IApiBaseRequest<GetInitialMyOrdersResponse>
{

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}
