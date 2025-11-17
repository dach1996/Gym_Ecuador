namespace LogicApi.Model.Response.GymSubscriptionPlan;

/// <summary>
/// Respuesta de obtener plan de suscripción por GUID
/// </summary>
public class GetGymSubscriptionPlanByGuidResponse : IApiBaseResponse
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    public bool ShowMessage { get; set; }

    /// <summary>
    /// Datos del plan de suscripción
    /// </summary>
    public GymSubscriptionPlanDetail Plan { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="plan"></param>
    public GetGymSubscriptionPlanByGuidResponse(GymSubscriptionPlanDetail plan)
    {
        Plan = plan;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetGymSubscriptionPlanByGuidResponse()
    {
    }
}

/// <summary>
/// Detalle completo de plan de suscripción
/// </summary>
public class GymSubscriptionPlanDetail
{
    /// <summary>
    /// Guid del plan
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Id del gimnasio
    /// </summary>
    public int GymId { get; set; }

    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Nombre del gimnasio
    /// </summary>
    public string GymName { get; set; }

    /// <summary>
    /// Nombre del plan
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Código del plan
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Descripción del plan
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Precio del plan
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Duración en días
    /// </summary>
    public int DurationDays { get; set; }

    /// <summary>
    /// Precio de inscripción
    /// </summary>
    public decimal? EnrollmentFee { get; set; }

    /// <summary>
    /// Estado activo
    /// </summary>
    public bool IsActive { get; set; }
}

