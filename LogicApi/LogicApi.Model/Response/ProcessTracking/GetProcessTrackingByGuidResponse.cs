using LogicApi.Model.Common;
using LogicCommon.Model.Response.File;

namespace LogicApi.Model.Response.ProcessTracking;

/// <summary>
/// Respuesta de obtener seguimiento de proceso por GUID
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="processTracking"></param>
public class GetProcessTrackingByGuidResponse(ProcessTrackingDetail processTracking) : IApiBaseResponse
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
    /// Detalle del seguimiento de proceso
    /// </summary>
    public ProcessTrackingDetail ProcessTracking { get; set; } = processTracking;
}

/// <summary>
/// Detalle del seguimiento de proceso
/// </summary>
public class ProcessTrackingDetail 
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
    /// Información del gimnasio
    /// </summary>
    public GymInfo Gym { get; set; }

     /// <summary>
    /// Listado de medidas de peso
    /// </summary>
    public List<StatisticComparisonModel> WeightMeasurements { get; set; } = [];

    /// <summary>
    /// Listado de medidas
    /// </summary>
    public List<StatisticComparisonModel> Measurements { get; set; } = [];

    /// <summary>
    /// Comentarios u observaciones del entrenador o la persona.
    /// </summary>
    public string Observations { get; set; }

    /// <summary>
    /// Imágenes del seguimiento de proceso
    /// </summary>
    public List<FileUrlResponse> Images { get; set; }
}

/// <summary>
/// Información del gimnasio
/// </summary>
public class GymInfo
{
    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre del gimnasio
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Dirección del gimnasio
    /// </summary>
    public string Address { get; set; }
}
