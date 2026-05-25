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
    /// Peso corporal actual (en kg o la unidad estándar)
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Altura de la persona (en cm) - No cambia con frecuencia, pero es clave para el IMC.
    /// </summary>
    public decimal Height { get; set; }

    /// <summary>
    /// Porcentaje de grasa corporal estimado.
    /// </summary>
    public decimal? BodyFatPercentage { get; set; }

    /// <summary>
    /// Porcentaje de masa muscular.
    /// </summary>
    public decimal? MuscleMassPercentage { get; set; }

    /// <summary>
    /// Circunferencia del pecho o tórax (cm).
    /// </summary>
    public decimal? ChestMeasurement { get; set; }

    /// <summary>
    /// Circunferencia de la cintura (cm). Clave para salud cardiovascular.
    /// </summary>
    public decimal? WaistMeasurement { get; set; }

    /// <summary>
    /// Circunferencia de la cadera (cm).
    /// </summary>
    public decimal? HipMeasurement { get; set; }

    /// <summary>
    /// Circunferencia del brazo derecho (cm).
    /// </summary>
    public decimal? ArmRightMeasurement { get; set; }

    /// <summary>
    /// Circunferencia del muslo derecho (cm).
    /// </summary>
    public decimal? ThighRightMeasurement { get; set; }

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

