using LogicApi.Model.Response.GymBranchSchedule;

namespace LogicApi.Model.Request.GymBranchSchedule;

/// <summary>
/// Solicitud para eliminar horario de sucursal
/// </summary>
public class DeleteGymBranchScheduleRequest : IRequest<DeleteGymBranchScheduleResponse>, IApiBaseRequest
{
    /// <summary>
    /// Id del horario
    /// </summary>
    [Required]
    public int ScheduleId { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public DeleteGymBranchScheduleRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public DeleteGymBranchScheduleRequest()
    {
    }
}

