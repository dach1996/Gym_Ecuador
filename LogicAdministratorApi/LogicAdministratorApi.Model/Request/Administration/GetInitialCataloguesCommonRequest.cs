using Common.WebCommon.Models;
using LogicCommon.Model.Request.Administration;
using LogicCommon.Model.Response.Administration;

namespace LogicAdministratorApi.Model.Request.Administration;
/// <summary>
/// Request para obtener los catálogos iniciales
/// </summary>
public class GetInitialCataloguesRequest : GetInitialCataloguesCommonRequest, IApiBaseRequest<GetInitialCataloguesResponse>
{

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
