using Common.WebApi.Models.ContextRequestModel;
using Common.WebCommon.Models;
using LogicApi.Model.Response.Administration;
using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Administration;
/// <summary>
/// Obtiene los catálogos iniciales
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="contextRequest"></param>
public class GetCooperativeInformationRequest(ContextRequest contextRequest) : IApiBaseRequest<GetCooperativeInformationResponse>
{

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; } = contextRequest;
}