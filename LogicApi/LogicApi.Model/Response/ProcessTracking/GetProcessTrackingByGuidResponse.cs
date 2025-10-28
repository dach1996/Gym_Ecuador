namespace LogicApi.Model.Response.ProcessTracking;

/// <summary>
/// Respuesta de obtener seguimiento de proceso por GUID
/// </summary>
public class GetProcessTrackingByGuidResponse : IApiBaseResponse
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
    public ProcessTrackingDetail ProcessTracking { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="processTracking"></param>
    public GetProcessTrackingByGuidResponse(ProcessTrackingDetail processTracking)
    {
        ProcessTracking = processTracking;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetProcessTrackingByGuidResponse()
    {
    }
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
    /// Información del gimnasio
    /// </summary>
    public GymInfo Gym { get; set; }

    /// <summary>
    /// Información de la persona
    /// </summary>
    public PersonInfo Person { get; set; }

    /// <summary>
    /// Tipo de proceso
    /// </summary>
    public string ProcessType { get; set; }

    /// <summary>
    /// Nombre del proceso
    /// </summary>
    public string ProcessName { get; set; }

    /// <summary>
    /// Descripción del proceso
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Estado del proceso
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Fecha de inicio
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de fin
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Progreso (porcentaje)
    /// </summary>
    public decimal? Progress { get; set; }

    /// <summary>
    /// Notas adicionales
    /// </summary>
    public string Notes { get; set; }

    /// <summary>
    /// Estado activo
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime? DateTimeRegister { get; set; }
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

/// <summary>
/// Información de la persona
/// </summary>
public class PersonInfo
{
    /// <summary>
    /// Id de la persona
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre completo
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; }
}
