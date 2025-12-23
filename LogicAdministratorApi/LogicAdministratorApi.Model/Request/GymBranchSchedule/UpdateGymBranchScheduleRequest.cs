using Common.WebApi.Models.ContextRequestModel;
using LogicAdministratorApi.Model.Response.GymBranchSchedule;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.GymBranchSchedule;

/// <summary>
/// Solicitud para actualizar horario de sucursal
/// </summary>
public class UpdateGymBranchScheduleRequest : IRequest<UpdateGymBranchScheduleResponse>, IApiBaseRequest
{
    /// <summary>
    /// Id del horario
    /// </summary>
    [Required]
    public int ScheduleId { get; set; }

    /// <summary>
    /// Hora de apertura
    /// </summary>
    public TimeSpan? OpeningTime { get; set; }

    /// <summary>
    /// Hora de cierre
    /// </summary>
    public TimeSpan? ClosingTime { get; set; }

    /// <summary>
    /// Indica si debe visualizarse
    /// </summary>
    public bool? IsVisible { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public UpdateGymBranchScheduleRequest(AdminContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateGymBranchScheduleRequest()
    {
    }
}

