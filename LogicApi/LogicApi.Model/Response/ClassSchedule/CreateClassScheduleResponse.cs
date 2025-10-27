namespace LogicApi.Model.Response.ClassSchedule;

/// <summary>
/// Respuesta de crear horario de clase
/// </summary>
public class CreateClassScheduleResponse : IApiBaseResponse
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
    /// Guid del horario de clase creado
    /// </summary>
    public Guid ClassScheduleGuid { get; set; }

    /// <summary>
    /// Día de la semana
    /// </summary>
    public string DayOfWeek { get; set; }

    /// <summary>
    /// Hora de inicio
    /// </summary>
    public TimeSpan StartTime { get; set; }

    /// <summary>
    /// Hora de fin
    /// </summary>
    public TimeSpan EndTime { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="classScheduleGuid"></param>
    /// <param name="dayOfWeek"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    public CreateClassScheduleResponse(Guid classScheduleGuid, string dayOfWeek, TimeSpan startTime, TimeSpan endTime)
    {
        ClassScheduleGuid = classScheduleGuid;
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateClassScheduleResponse()
    {
    }
}
