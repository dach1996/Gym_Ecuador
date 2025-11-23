using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.GymBranchSchedule;

using Common.WebCommon.Models;
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
    public CommonContextRequest ContextRequest { get; set; }

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

