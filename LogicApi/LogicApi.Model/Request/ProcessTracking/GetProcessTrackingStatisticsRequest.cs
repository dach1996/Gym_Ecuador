using LogicApi.Model.Response.ProcessTracking;

using Common.WebCommon.Models;

namespace LogicApi.Model.Request.ProcessTracking;

/// <summary>
/// Solicitud para obtener estadísticas de seguimientos de procesos
/// </summary>
public class GetProcessTrackingStatisticsRequest : IApiBaseRequest<GetProcessTrackingStatisticsResponse>
{
    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Fecha de inicio
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de fin
    /// </summary>
    public DateTime EndDate { get; set; }
}
