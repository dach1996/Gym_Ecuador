namespace LogicAdministratorApi.Model.Response.GymBranchSchedule;

/// <summary>
/// Respuesta de eliminar horario de sucursal
/// </summary>
public class DeleteGymBranchScheduleResponse : IApiBaseResponse
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
    /// Indica si se eliminó exitosamente
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="success"></param>
    public DeleteGymBranchScheduleResponse(bool success)
    {
        Success = success;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public DeleteGymBranchScheduleResponse()
    {
    }
}

