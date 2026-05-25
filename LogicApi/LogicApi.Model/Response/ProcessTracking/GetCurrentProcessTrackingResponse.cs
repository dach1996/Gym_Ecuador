using LogicApi.Model.Common;

namespace LogicApi.Model.Response.ProcessTracking;

/// <summary>
/// Respuesta de obtener el seguimiento de proceso más reciente del usuario
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="processTracking"></param>
public class GetCurrentProcessTrackingResponse(CurrentProcessTrackingDetail processTracking) : IApiBaseResponse
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
    /// Detalle del seguimiento de proceso más reciente
    /// </summary>
    public CurrentProcessTrackingDetail ProcessTracking { get; set; } = processTracking;
}

/// <summary>
/// Detalle del seguimiento de proceso más reciente
/// </summary>
public class CurrentProcessTrackingDetail
{
    /// <summary>
    /// Estadísticas de comparación
    /// </summary>
    /// <value></value>
    public List<StatisticComparisonModel> Statistics { get; set; } = [];
}
