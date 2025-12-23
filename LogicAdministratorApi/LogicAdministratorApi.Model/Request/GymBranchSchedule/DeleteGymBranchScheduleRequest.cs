using Common.WebApi.Models.ContextRequestModel;
using LogicAdministratorApi.Model.Response.GymBranchSchedule;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.GymBranchSchedule;

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
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public DeleteGymBranchScheduleRequest(AdminContextRequest contextRequest)
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

