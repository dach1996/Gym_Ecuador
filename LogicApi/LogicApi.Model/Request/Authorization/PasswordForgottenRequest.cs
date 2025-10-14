using LogicApi.Model.Response.Authorization;
namespace LogicApi.Model.Request.Authorization;

/// <summary>
/// Request de recuperación de contraseña
/// </summary>
public class PasswordForgottenRequest : IApiBaseRequest<PasswordForgottenResponse> 
{
    /// <summary>
    /// Correo
    /// </summary>
    [Required]
    public string Email { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}
