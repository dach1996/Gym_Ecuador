using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.UserAdministrator;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.UserAdministrator;

/// <summary>
/// Solicitud para obtener detalle de usuario administrador por GUID
/// </summary>
public class GetUserAdministratorByGuidRequest : IApiBaseRequest<GetUserAdministratorByGuidResponse>
{
    /// <summary>
    /// Guid del usuario administrador
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid UserGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

