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
    /// Peso corporal actual (en kg o la unidad estándar)
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Peso corporal anterior (en kg o la unidad estándar)
    /// </summary>
    public decimal? PreviousWeight { get; set; }

    /// <summary>
    /// Porcentaje de diferencia de peso
    /// </summary>
    public decimal WeightPercentageDifference { get; set; }

    /// <summary>
    /// Porcentaje de grasa corporal estimado.
    /// </summary>
    public decimal? BodyFatPercentage { get; set; }

    /// <summary>
    /// Porcentaje de grasa corporal anterior.
    /// </summary>
    public decimal? PreviousBodyFatPercentage { get; set; }

    /// <summary>
    /// Porcentaje de diferencia de grasa corporal
    /// </summary>
    public decimal? BodyFatPercentageDifference { get; set; }

    /// <summary>
    /// Índice de masa corporal.
    /// </summary>
    public decimal Bmi { get; set; }

    /// <summary>
    /// Porcentaje de diferencia de IMC
    /// </summary>
    public decimal? PreviousBmi { get; set; }

    /// <summary>
    /// Porcentaje de diferencia de IMC
    /// </summary>
    public decimal BmiDifference { get; set; }
}
