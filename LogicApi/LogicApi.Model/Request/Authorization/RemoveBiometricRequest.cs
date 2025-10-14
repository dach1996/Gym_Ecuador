using LogicApi.Model.Response;

namespace LogicApi.Model.Request.Authorization;
/// <summary>
/// Request para eliminar Biométrico
/// </summary>
public class RemoveBiometricRequest : IApiBaseRequest<HandlerResponse>
{

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Biométrico
    /// </summary>
    [Required]
    public string Biometric { get; set; }
}