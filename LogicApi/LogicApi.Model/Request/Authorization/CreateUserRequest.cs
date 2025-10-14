using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicApi.Model.Implementations.Authorization;
using LogicApi.Model.Response.Authorization;
namespace LogicApi.Model.Request.Authorization;
/// <summary>
/// Request de Crear Usuarios
/// </summary>
public class CreateUserRequest : IApiBaseRequest<CreateUserResponse>
{
    /// <summary>
    /// Correo
    /// </summary>
    /// <value></value>
    [Required]
    [MaxLength(128)]
    public string Email { get; set; }

    /// <summary>
    /// Términos y condiciones
    /// </summary>
    /// <value></value>
    [Required]
    public bool ConditionAndTermines { get; set; }

    /// <summary>
    /// Contraseña (Encriptada)
    /// </summary>
    [ValidateEnum]
    public LoginImplementations LoginType { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}