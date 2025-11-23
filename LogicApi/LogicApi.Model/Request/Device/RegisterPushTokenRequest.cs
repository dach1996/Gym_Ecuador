using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Device;

/// <summary>
/// Actualización de token para notificaciones Push
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="contextRequest"></param>
/// <param name="token"></param>
public class RegisterPushTokenRequest(ContextRequest contextRequest, string token) : IApiBaseRequest<HandlerResponse>
{
    /// <summary>
    /// Token para notificaciones push
    /// </summary>
    [Required]
    public string Token { get; set; } = token;
 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; } = contextRequest;
}
