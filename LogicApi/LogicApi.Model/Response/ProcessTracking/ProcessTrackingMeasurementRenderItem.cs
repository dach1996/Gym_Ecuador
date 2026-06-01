namespace LogicApi.Model.Response.ProcessTracking;

/// <summary>
/// Item de medida para renderizado del formulario de seguimiento de proceso
/// </summary>
public class ProcessTrackingMeasurementRenderItem
{
    /// <summary>
    /// Código del parámetro físico (PAF_CODIGO)
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Etiqueta descriptiva del parámetro
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// Unidad de medida
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// Código del icono para UI
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// Indica si la medida es obligatoria en el formulario
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    /// Valor de la última medición registrada, o null si no existe
    /// </summary>
    public decimal? CurrentValue { get; set; }
}
