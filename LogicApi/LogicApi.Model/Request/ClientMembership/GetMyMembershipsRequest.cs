using LogicApi.Model.Response.ClientMembership;
using Common.WebCommon.Models;

namespace LogicApi.Model.Request.ClientMembership;

/// <summary>
/// Solicitud para obtener mis membresías agrupadas por sucursal
/// </summary>
public class GetMyMembershipsRequest : IApiBaseRequest<GetMyMembershipsResponse>
{
    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
