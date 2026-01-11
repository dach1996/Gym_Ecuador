using LogicAdministratorApi.Model.Response.Role;
using Common.WebCommon.Models;
using Common.WebCommon.Attributes.CustomDataAnnotations;

namespace LogicAdministratorApi.Model.Request.Role;

/// <summary>
/// Solicitud para obtener detalle de rol por GUID
/// </summary>
public class GetRoleDetailRequest : IApiBaseRequest<GetRoleDetailResponse>
{
    /// <summary>
    /// Guid del rol
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid RoleGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
