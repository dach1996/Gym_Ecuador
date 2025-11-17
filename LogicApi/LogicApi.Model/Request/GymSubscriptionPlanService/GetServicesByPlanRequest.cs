using LogicApi.Model.Response.GymSubscriptionPlanService;

namespace LogicApi.Model.Request.GymSubscriptionPlanService;

/// <summary>
/// Solicitud para obtener servicios de un plan de suscripción
/// </summary>
public class GetServicesByPlanRequest : IRequest<GetServicesByPlanResponse>, IApiBaseRequest
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
    public GetServicesByPlanRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetServicesByPlanRequest()
    {
    }
}

