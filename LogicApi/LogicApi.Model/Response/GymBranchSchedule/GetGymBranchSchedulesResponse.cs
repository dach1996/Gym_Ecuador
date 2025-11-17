namespace LogicApi.Model.Response.GymBranchSchedule;

/// <summary>
/// Respuesta de obtener horarios de sucursal
/// </summary>
public class GetGymBranchSchedulesResponse : IApiBaseResponse
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
    /// Nombre de la sucursal
    /// </summary>
    public string BranchName { get; set; }

    /// <summary>
    /// Lista de horarios
    /// </summary>
    public IEnumerable<GymBranchScheduleItem> Schedules { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="branchName"></param>
    /// <param name="schedules"></param>
    public GetGymBranchSchedulesResponse(string branchName, IEnumerable<GymBranchScheduleItem> schedules)
    {
        BranchName = branchName;
        Schedules = schedules;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetGymBranchSchedulesResponse()
    {
    }
}

/// <summary>
/// Item de horario de sucursal
/// </summary>
public class GymBranchScheduleItem
{
    /// <summary>
    /// Id del horario
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Id del día de la semana
    /// </summary>
    public int DayOfWeekCatalogId { get; set; }

    /// <summary>
    /// Nombre del día de la semana
    /// </summary>
    public string DayOfWeekName { get; set; }

    /// <summary>
    /// Hora de apertura
    /// </summary>
    public TimeSpan OpeningTime { get; set; }

    /// <summary>
    /// Hora de cierre
    /// </summary>
    public TimeSpan ClosingTime { get; set; }

    /// <summary>
    /// Es visible
    /// </summary>
    public bool IsVisible { get; set; }
}

