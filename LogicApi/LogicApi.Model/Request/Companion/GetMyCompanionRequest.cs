using LogicApi.Model.Response.Companion;

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
    public ContextRequest ContextRequest { get; set; }
}