using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.GymSubscriptionPlan;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.GymSubscriptionPlan;

/// <summary>
/// Solicitud para crear un plan de suscripción
/// </summary>
public class CreateGymSubscriptionPlanRequest : IRequest<CreateGymSubscriptionPlanResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    [Required]
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Nombre del plan
    /// </summary>
    [Required]
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
    [Required]
    public decimal Price { get; set; }

    /// <summary>
    /// Duración del plan en días
    /// </summary>
    [Required]
    public int DurationDays { get; set; }

    /// <summary>
    /// Precio de inscripción
    /// </summary>
    public decimal? EnrollmentFee { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public CreateGymSubscriptionPlanRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateGymSubscriptionPlanRequest()
    {
    }
}

