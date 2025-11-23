using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.GymSubscriptionPlanService;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.GymSubscriptionPlanService;

/// <summary>
/// Solicitud para remover un servicio de un plan de suscripción
/// </summary>
public class RemoveServiceFromPlanRequest : IRequest<RemoveServiceFromPlanResponse>, IApiBaseRequest
{
    /// <summary>
    /// Id de la relación plan-servicio
    /// </summary>
    [Required]
    public int PlanServiceId { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public RemoveServiceFromPlanRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public RemoveServiceFromPlanRequest()
    {
    }
}

