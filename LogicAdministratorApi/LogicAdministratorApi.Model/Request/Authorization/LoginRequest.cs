using Common.WebCommon.Models;
using LogicAdministratorApi.Model.Response.Authorization;
namespace LogicAdministratorApi.Model.Request.Authorization;

/// <summary>
/// Objeto para Ingreso al sistema
/// </summary>
public class LoginRequest : IApiBaseRequest<LoginResponse>
{
    /// <summary>
    /// Nombre de Usuario
    /// </summary>
    /// <value></value>
    [Required]
    [MaxLength(1024)]
    [EncryptedField]
    public string Username { get; set; }
    
    /// <summary>
    /// Contraseña
    /// </summary>
    /// <value></value>
    [MaxLength(1024)]
    [Required]
    [EncryptedField]
    public string Password { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
