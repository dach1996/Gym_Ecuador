using Common.WebCommon.Models;
using LogicAdministratorApi.Model.Response.Role;

namespace LogicAdministratorApi.Model.Request.Role;

/// <summary>
/// Solicitud para obtener todos los roles
/// </summary>
public class GetRolesRequest : IApiBaseRequest<GetRolesResponse>
{
    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
