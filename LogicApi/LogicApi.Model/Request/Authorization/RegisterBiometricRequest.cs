using LogicApi.Model.Response.Authorization;

namespace LogicApi.Model.Request.Authorization;
/// <summary>
/// Request para registrar Biométrico
/// </summary>
public class RegisterBiometricRequest : IApiBaseRequest<RegisterBiometricResponse>
{
    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public RegisterBiometricRequest()
    {
    }
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public RegisterBiometricRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }
}