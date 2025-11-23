using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Order;
using Common.WebCommon.Models;
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
    public CommonContextRequest ContextRequest { get; set; }
}
