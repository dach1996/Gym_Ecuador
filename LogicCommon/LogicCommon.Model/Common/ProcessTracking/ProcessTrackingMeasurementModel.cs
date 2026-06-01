namespace LogicCommon.Model.Common.ProcessTracking;

/// <summary>
/// Medida de seguimiento de proceso para lectura
/// </summary>
public class ProcessTrackingMeasurementModel
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
    /// Valor de la última medición registrada, o null si no existe
    /// </summary>
    public decimal Value { get; set; }
}
