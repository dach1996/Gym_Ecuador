using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Authorization;

using Common.WebCommon.Models;
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
    public CommonContextRequest ContextRequest { get; set; }

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