using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.GymSubscriptionPlan;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.GymSubscriptionPlan;

/// <summary>
/// Solicitud para actualizar un plan de suscripción
/// </summary>
public class UpdateGymSubscriptionPlanRequest : IRequest<UpdateGymSubscriptionPlanResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del plan de suscripción
    /// </summary>
    [Required]
    public Guid PlanGuid { get; set; }

    /// <summary>
    /// Nombre del plan
    /// </summary>
    [StringLength(200)]
    public string Name { get; set; }

    /// <summary>
    /// Código único del plan
    /// </summary>
    [StringLength(50)]
    public string Code { get; set; }

    /// <summary>
    /// Descripción del plan
    /// </summary>
    [StringLength(1000)]
    public string Description { get; set; }

    /// <summary>
    /// Precio del plan
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    /// Duración del plan en días
    /// </summary>
    public int? DurationDays { get; set; }

    /// <summary>
    /// Precio de inscripción
    /// </summary>
    public decimal? EnrollmentFee { get; set; }

    /// <summary>
    /// Estado del plan
    /// </summary>
    public bool? IsActive { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public UpdateGymSubscriptionPlanRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateGymSubscriptionPlanRequest()
    {
    }
}

