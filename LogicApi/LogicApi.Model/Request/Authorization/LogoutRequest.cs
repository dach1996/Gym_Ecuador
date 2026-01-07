using LogicApi.Model.Response;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Authorization;
/// <summary>
/// Request Logout
/// </summary>
public class LogoutRequest : IApiBaseRequest<HandlerResponse>
{

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}