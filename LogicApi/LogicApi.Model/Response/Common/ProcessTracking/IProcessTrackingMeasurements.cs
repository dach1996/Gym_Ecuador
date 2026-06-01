using LogicCommon.Model.Common.ProcessTracking;

namespace LogicApi.Model.Response.Common.ProcessTracking;

/// <summary>
/// Contrato de medidas en responses de seguimiento de proceso
/// </summary>
public interface IProcessTrackingMeasurements
{
    /// <summary>
    /// Listado de medidas
    /// </summary>
    List<ProcessTrackingMeasurementModel> Measurements { get; set; }
}
