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
    /// Nombre de usuario
    /// </summary>
    [StringLength(100)]
    public string UserName { get; set; }

    /// <summary>
    /// Email del usuario
    /// </summary>
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }

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
    /// Código de idioma
    /// </summary>
    [StringLength(50)]
    public string LanguageCode { get; set; }

    /// <summary>
    /// Indica si el usuario está bloqueado
    /// </summary>
    public bool? IsBlocked { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

