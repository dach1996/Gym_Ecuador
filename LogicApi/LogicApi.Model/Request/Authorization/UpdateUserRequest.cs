using LogicApi.Model.Common;
using LogicApi.Model.Response;
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
    public ContextRequest ContextRequest { get; set; }
}