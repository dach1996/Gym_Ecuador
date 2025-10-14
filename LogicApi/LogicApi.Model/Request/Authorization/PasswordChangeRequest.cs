using LogicApi.Model.Response;

namespace LogicApi.Model.Request.Authorization;

/// <summary>
/// Request de cambio de contraseña
/// </summary>
public class PasswordChangeRequest : IRequest<HandlerResponse>, IApiBaseRequest
{
    /// <summary>
    /// Contraseña encriptado
    /// </summary>
    [Required]
    [EncryptedField]
    public string CurrentPassword { get; set; }

    /// <summary>
    /// Nueva contraseña encriptado
    /// </summary>
    [Required]
    [EncryptedField]
    public string NewPassword { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}
