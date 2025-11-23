using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Companion;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Companion;
/// <summary>
/// Request de obtener mis compañero de viaje
/// </summary>
public class GetMyCompanionRequest : IApiBaseRequest<GetMyCompanionResponse>
{
 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}