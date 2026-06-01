
using LogicCommon.Model.Common.ProcessTracking;
using LogicCommon.Model.Response.File;

namespace LogicApi.Model.Response.ProcessTracking;

/// <summary>
/// Item de seguimiento de proceso
/// </summary>
public class ProcessTrackingItem
{
    /// <summary>
    /// Guid del seguimiento de proceso
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime RegistrationDate { get; set; }

    /// <summary>
    /// Listado de medidas principales
    /// </summary>
    public List<ProcessTrackingMeasurementModel> PrincipalMeasurements { get; set; } = [];

    /// <summary>
    /// Listado de medidas secundarias
    /// </summary>
    public List<ProcessTrackingMeasurementModel> SecondaryMeasurements { get; set; } = [];

    /// <summary>
    /// Lista de imágenes asociadas al seguimiento de proceso.
    /// </summary>
    public List<FileUrlResponse> Images { get; set; } = [];
}
