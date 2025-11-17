namespace LogicApi.Model.Response.GymBranchSchedule;

/// <summary>
/// Respuesta de actualizar horario de sucursal
/// </summary>
public class UpdateGymBranchScheduleResponse : IApiBaseResponse
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
    /// Id del horario
    /// </summary>
    public int ScheduleId { get; set; }

    /// <summary>
    /// Indica si se actualizó exitosamente
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="scheduleId"></param>
    /// <param name="success"></param>
    public UpdateGymBranchScheduleResponse(int scheduleId, bool success)
    {
        ScheduleId = scheduleId;
        Success = success;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateGymBranchScheduleResponse()
    {
    }
}

