using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Authorization;
using Common.WebCommon.Models;
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
    public CommonContextRequest ContextRequest { get; set; }
}
