using Common.WebCommon.Models;
using LogicApi.Model.Response.ProcessTracking;

namespace LogicApi.Model.Request.ProcessTracking;

/// <summary>
/// Solicitud para obtener el seguimiento de proceso más reciente del usuario
/// </summary>
public class GetCurrentProcessTrackingRequest : IApiBaseRequest<GetCurrentProcessTrackingResponse>
{
    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
