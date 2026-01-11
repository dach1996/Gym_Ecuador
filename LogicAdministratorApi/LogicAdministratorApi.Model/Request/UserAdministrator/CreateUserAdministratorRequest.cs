using LogicAdministratorApi.Model.Response.UserAdministrator;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.UserAdministrator;

/// <summary>
/// Solicitud para crear un usuario administrador
/// </summary>
public class CreateUserAdministratorRequest : IApiBaseRequest<CreateUserAdministratorResponse>
{
    /// <summary>
    /// Nombre de usuario
    /// </summary>
    [StringLength(100)]
    public string UserName { get; set; }

    /// <summary>
    /// Número de identificación del usuario
    /// </summary>
    [Required]
    [StringLength(50)]
    public string IdentificationNumber { get; set; }

    /// <summary>
    /// Email del usuario
    /// </summary>
    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Teléfono del usuario
    /// </summary>
    [StringLength(20)]
    public string Phone { get; set; }

    /// <summary>
    /// Contraseña del usuario
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Password { get; set; }

    /// <summary>
    /// Código de idioma
    /// </summary>
    [StringLength(50)]
    public string LanguageCode { get; set; }

    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Lista de GUIDs de roles a asignar al usuario
    /// </summary>
    public List<Guid> RoleGuids { get; set; } = new();

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

