namespace LogicApi.Model.Request.ProcessTracking;

/// <summary>
/// Contrato de medidas en requests de seguimiento de proceso
/// </summary>
public interface IProcessTrackingMeasurementsInput
{
    /// <summary>
    /// Listado de medidas (código + valor)
    /// </summary>
    List<ProcessTrackingMeasurementInput> Measurements { get; }
}
