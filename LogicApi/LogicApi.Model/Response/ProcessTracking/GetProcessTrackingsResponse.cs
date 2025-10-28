namespace LogicApi.Model.Response.ProcessTracking;

/// <summary>
/// Respuesta de obtener seguimientos de procesos
/// </summary>
public class GetProcessTrackingsResponse : IApiBaseResponse
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
    /// Lista de seguimientos de procesos
    /// </summary>
    public IEnumerable<ProcessTrackingItem> ProcessTrackings { get; set; }

    /// <summary>
    /// Total de registros
    /// </summary>
    public int TotalRecords { get; set; }

    /// <summary>
    /// Página actual
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Total de páginas
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="processTrackings"></param>
    /// <param name="totalRecords"></param>
    /// <param name="currentPage"></param>
    /// <param name="pageSize"></param>
    public GetProcessTrackingsResponse(IEnumerable<ProcessTrackingItem> processTrackings, int totalRecords, int currentPage, int pageSize)
    {
        ProcessTrackings = processTrackings;
        TotalRecords = totalRecords;
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetProcessTrackingsResponse()
    {
    }
}

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
    /// Nombre completo de la persona
    /// </summary>
    public string PersonFullName { get; set; }

    /// <summary>
    /// Tipo de proceso
    /// </summary>
    public string ProcessType { get; set; }

    /// <summary>
    /// Nombre del proceso
    /// </summary>
    public string ProcessName { get; set; }

    /// <summary>
    /// Estado del proceso
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Progreso (porcentaje)
    /// </summary>
    public decimal? Progress { get; set; }

    /// <summary>
    /// Nombre del gimnasio
    /// </summary>
    public string GymName { get; set; }

    /// <summary>
    /// Fecha de inicio
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de fin
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Estado activo
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime? DateTimeRegister { get; set; }
}
