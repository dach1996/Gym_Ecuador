using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Common;
using LogicApi.Model.Response;
using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Authorization;
/// <summary>
/// Request Logout
/// </summary>
public class UpdateUserRequest : IRequest<HandlerResponse>, IApiBaseRequest
{
    /// <summary>
    /// Teléfono
    /// </summary>
    /// <value></value>
    [Required]
    public string Phone { get; set; }

    /// <summary>
    /// Imagen
    /// </summary>
    /// <value></value>
    public RequestEncodeFile Image { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}