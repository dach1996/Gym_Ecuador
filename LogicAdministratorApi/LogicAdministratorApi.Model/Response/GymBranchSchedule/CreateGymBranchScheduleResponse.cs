namespace LogicAdministratorApi.Model.Response.GymBranchSchedule;

/// <summary>
/// Respuesta de crear horario de sucursal
/// </summary>
public class CreateGymBranchScheduleResponse : IApiBaseResponse
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
    /// Id del horario creado
    /// </summary>
    public int ScheduleId { get; set; }

    /// <summary>
    /// Nombre del día de la semana
    /// </summary>
    public string DayOfWeek { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="scheduleId"></param>
    /// <param name="dayOfWeek"></param>
    public CreateGymBranchScheduleResponse(int scheduleId, string dayOfWeek)
    {
        ScheduleId = scheduleId;
        DayOfWeek = dayOfWeek;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateGymBranchScheduleResponse()
    {
    }
}

