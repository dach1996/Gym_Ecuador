namespace LogicApi.Model.Request.ProcessTracking;

/// <summary>
/// Medida de seguimiento de proceso (código de catálogo + valor)
/// </summary>
public class ProcessTrackingMeasurementInput
{
    /// <summary>
    /// Código del parámetro físico (PAF_CODIGO: PESO, ALTURA, CINTURA, …)
    /// </summary>
    [Required]
    public string Code { get; set; }

    /// <summary>
    /// Valor registrado de la medida
    /// </summary>
    [Required]
    [Range(0.01, 999.99)]
    public decimal Value { get; set; }
}
