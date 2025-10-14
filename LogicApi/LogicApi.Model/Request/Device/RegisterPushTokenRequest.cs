using LogicApi.Model.Response;

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
    public ContextRequest ContextRequest { get; set; } = contextRequest;
}
