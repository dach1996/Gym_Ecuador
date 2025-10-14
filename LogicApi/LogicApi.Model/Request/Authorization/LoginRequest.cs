using LogicApi.Model.Implementations.Authorization;
using LogicApi.Model.Response.Authorization;
namespace LogicApi.Model.Request.Authorization;

/// <summary>
/// Objeto para Ingreso al sistema
/// </summary>
public class LoginRequest : IRequest<LoginResponse>, IApiBaseRequest
{
    /// <summary>
    /// Usuario (Encriptado)
    /// </summary>
    [EncryptedField]
    [Required]
    public string UserName { get; set; }

    /// <summary>
    /// Contraseña (Encriptada)
    /// </summary>
    [Required]
    public string Password { get; set; }
    
    /// <summary>
    /// Token para Push
    /// </summary>
    public string PushToken { get; set; }

    /// <summary>
    /// Contraseña (Encriptada)
    /// </summary>
    [Required]
    public LoginImplementations LoginType { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}
