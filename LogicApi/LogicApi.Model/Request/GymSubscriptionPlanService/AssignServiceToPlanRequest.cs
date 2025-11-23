using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.GymSubscriptionPlanService;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.GymSubscriptionPlanService;

/// <summary>
/// Solicitud para asignar un servicio a un plan de suscripción
/// </summary>
public class AssignServiceToPlanRequest : IRequest<AssignServiceToPlanResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del plan de suscripción
    /// </summary>
    [Required]
    public Guid PlanGuid { get; set; }

    /// <summary>
    /// Id del servicio
    /// </summary>
    [Required]
    public int ServiceId { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public AssignServiceToPlanRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public AssignServiceToPlanRequest()
    {
    }
}

