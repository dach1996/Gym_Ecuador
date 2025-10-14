using LogicApi.Model.Response.Authorization;
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
    public ContextRequest ContextRequest { get; set; }
}