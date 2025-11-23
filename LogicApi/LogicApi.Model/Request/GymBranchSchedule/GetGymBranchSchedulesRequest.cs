using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.GymBranchSchedule;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.GymBranchSchedule;

/// <summary>
/// Solicitud para obtener horarios de sucursal
/// </summary>
public class GetGymBranchSchedulesRequest : IRequest<GetGymBranchSchedulesResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid de la sucursal de gimnasio
    /// </summary>
    [Required]
    public Guid GymBranchGuid { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public GetGymBranchSchedulesRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetGymBranchSchedulesRequest()
    {
    }
}

