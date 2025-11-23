using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.GymSubscriptionPlan;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.GymSubscriptionPlan;

/// <summary>
/// Solicitud para obtener un plan de suscripción por GUID
/// </summary>
public class GetGymSubscriptionPlanByGuidRequest : IRequest<GetGymSubscriptionPlanByGuidResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del plan de suscripción
    /// </summary>
    [Required]
    public Guid PlanGuid { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public GetGymSubscriptionPlanByGuidRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetGymSubscriptionPlanByGuidRequest()
    {
    }
}

