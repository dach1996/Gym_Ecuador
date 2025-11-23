using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response;

using Common.WebCommon.Models;
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
    public CommonContextRequest ContextRequest { get; set; }
}
