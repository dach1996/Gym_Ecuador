using LogicApi.Model.Response.Device;

namespace LogicApi.Model.Request.Device;

/// <summary>
/// Actualización de token para notificaciones Push
/// </summary>
public class RefreshTokenPushRequest : IRequest<RefreshTokenPushResponse>, IApiBaseRequest
{
    /// <summary>
    /// Token para notificaciones push
    /// </summary>
    [Required]
    public string PushToken { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}
