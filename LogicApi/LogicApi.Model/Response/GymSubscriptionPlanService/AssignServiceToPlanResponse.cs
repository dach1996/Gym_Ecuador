namespace LogicApi.Model.Response.GymSubscriptionPlanService;

/// <summary>
/// Respuesta de asignar servicio a plan
/// </summary>
public class AssignServiceToPlanResponse : IApiBaseResponse
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
    /// Id de la relación creada
    /// </summary>
    public int PlanServiceId { get; set; }

    /// <summary>
    /// Nombre del plan
    /// </summary>
    public string PlanName { get; set; }

    /// <summary>
    /// Nombre del servicio
    /// </summary>
    public string ServiceName { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="planServiceId"></param>
    /// <param name="planName"></param>
    /// <param name="serviceName"></param>
    public AssignServiceToPlanResponse(int planServiceId, string planName, string serviceName)
    {
        PlanServiceId = planServiceId;
        PlanName = planName;
        ServiceName = serviceName;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public AssignServiceToPlanResponse()
    {
    }
}

