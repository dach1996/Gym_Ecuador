using LogicApi.Model.Response.GymSubscriptionPlan;

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
    public ContextRequest ContextRequest { get; set; }

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

