using Common.WebCommon.Models;
using LogicApi.Model.Response.ProcessTracking;

namespace LogicApi.Model.Request.ProcessTracking;

/// <summary>
/// Solicitud para obtener items de medidas para renderizado del formulario
/// </summary>
public class GetProcessTrackingMeasurementRenderItemsRequest : IApiBaseRequest<GetProcessTrackingMeasurementRenderItemsResponse>
{
    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
