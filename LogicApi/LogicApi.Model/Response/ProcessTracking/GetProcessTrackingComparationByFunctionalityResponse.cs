using LogicApi.Model.Common;

namespace LogicApi.Model.Response.ProcessTracking;

/// <summary>
/// Respuesta de comparación de seguimiento de proceso por funcionalidad
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="statistics"></param>
public class GetProcessTrackingComparationByFunctionalityResponse(List<StatisticComparisonModel> statistics) : IApiBaseResponse
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    public bool ShowMessage { get; set; }

    /// <summary>
    /// Estadísticas de comparación
    /// </summary>
        /// <value></value>
    public List<StatisticComparisonModel> Statistics { get; set; } = statistics;
}
