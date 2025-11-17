namespace LogicApi.Model.Response.GymSubscriptionPlan;

/// <summary>
/// Respuesta de actualizar plan de suscripción
/// </summary>
public class UpdateGymSubscriptionPlanResponse : IApiBaseResponse
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
    /// Guid del plan de suscripción
    /// </summary>
    public Guid PlanGuid { get; set; }

    /// <summary>
    /// Nombre del plan
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="planGuid"></param>
    /// <param name="name"></param>
    public UpdateGymSubscriptionPlanResponse(Guid planGuid, string name)
    {
        PlanGuid = planGuid;
        Name = name;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateGymSubscriptionPlanResponse()
    {
    }
}

