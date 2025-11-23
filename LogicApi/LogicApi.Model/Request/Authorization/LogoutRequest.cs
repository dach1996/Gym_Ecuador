using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response;

using Common.WebCommon.Models;
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
    public CommonContextRequest ContextRequest { get; set; }
}