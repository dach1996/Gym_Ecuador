using LogicApi.Model.Response.ClassSchedule;

namespace LogicApi.Model.Request.ClassSchedule;

/// <summary>
/// Solicitud para crear un horario de clase
/// </summary>
public class CreateClassScheduleRequest : IRequest<CreateClassScheduleResponse>, IApiBaseRequest
{
    /// <summary>
    /// Id de la clase grupal
    /// </summary>
    public int GroupClassId { get; set; }

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
    /// Ubicación de la sala
    /// </summary>
    public string RoomLocation { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public CreateClassScheduleRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateClassScheduleRequest()
    {
    }
}
