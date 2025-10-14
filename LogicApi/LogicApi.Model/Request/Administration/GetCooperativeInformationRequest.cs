using LogicApi.Model.Response.Administration;
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
    public ContextRequest ContextRequest { get; set; } = contextRequest;
}