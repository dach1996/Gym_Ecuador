using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Authorization;
using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Authorization;
/// <summary>
/// Regenerar Token
/// </summary>
public class RefreshTokenRequest: IRequest<RefreshTokenResponse>, IApiBaseRequest
{

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}