using Common.WebCommon.Models;
using LogicAdministratorApi.Model.Response.Role;

namespace LogicAdministratorApi.Model.Request.Role;

/// <summary>
/// Solicitud para crear un nuevo rol
/// </summary>
public class CreateRoleRequest : IApiBaseRequest<CreateRoleResponse>
{
    /// <summary>
    /// Nombre del rol
    /// </summary>
    [Required]
    [StringLength(32)]
    public string Name { get; set; }

    /// <summary>
    /// Descripción del rol
    /// </summary>
    [Required]
    [StringLength(64)]
    public string Description { get; set; }

    /// <summary>
    /// Alcance del rol
    /// </summary>
    [Required]
    public string ScopeCode { get; set; }

    /// <summary>
    /// Lista de GUIDs de funcionalidades a asignar al rol
    /// </summary>
    public List<Guid> FunctionalityGuids { get; set; } = new();

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
