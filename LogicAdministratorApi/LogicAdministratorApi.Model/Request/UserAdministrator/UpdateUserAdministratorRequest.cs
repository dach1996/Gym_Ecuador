using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.UserAdministrator;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.UserAdministrator;

/// <summary>
/// Solicitud para actualizar un usuario administrador
/// </summary>
public class UpdateUserAdministratorRequest : IApiBaseRequest<UpdateUserAdministratorResponse>
{
    /// <summary>
    /// GUID del usuario a actualizar
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid UserGuid { get; set; }

    /// <summary>
    /// Teléfono del usuario
    /// </summary>
    [StringLength(20)]
    public string Phone { get; set; }

    /// <summary>
    /// Contraseña del usuario (opcional, solo si se desea cambiar)
    /// </summary>
    [StringLength(100)]
    public string Password { get; set; }

    /// <summary>
    /// Indica si el usuario está bloqueado
    /// </summary>
    public bool? IsBlocked { get; set; }

    /// <summary>
    /// Lista de GUIDs de roles a asignar al usuario (opcional, si se proporciona reemplaza los roles existentes)
    /// </summary>
    public Guid RoleGuid { get; set; }

    /// <summary>
    /// Guid del gimnasio para los roles (opcional, solo necesario si se asignan roles con alcance Gym)
    /// </summary>
    public Guid? Identifier { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

