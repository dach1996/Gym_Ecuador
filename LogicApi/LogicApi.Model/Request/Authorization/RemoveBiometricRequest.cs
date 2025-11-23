using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response;

using Common.WebCommon.Models;
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
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Biométrico
    /// </summary>
    [Required]
    public string Biometric { get; set; }
}