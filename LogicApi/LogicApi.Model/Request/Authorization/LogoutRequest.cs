using LogicApi.Model.Response;

namespace LogicApi.Model.Request.Authorization;
/// <summary>
/// Request Logout
/// </summary>
public class LogoutRequest : IRequest<HandlerResponse>, IApiBaseRequest
{

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}