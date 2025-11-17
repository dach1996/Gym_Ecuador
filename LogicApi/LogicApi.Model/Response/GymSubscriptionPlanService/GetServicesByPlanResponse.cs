namespace LogicApi.Model.Response.GymSubscriptionPlanService;

/// <summary>
/// Respuesta de obtener servicios por plan
/// </summary>
public class GetServicesByPlanResponse : IApiBaseResponse
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
    /// Nombre del plan
    /// </summary>
    public string PlanName { get; set; }

    /// <summary>
    /// Lista de servicios del plan
    /// </summary>
    public IEnumerable<PlanServiceItem> Services { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="planName"></param>
    /// <param name="services"></param>
    public GetServicesByPlanResponse(string planName, IEnumerable<PlanServiceItem> services)
    {
        PlanName = planName;
        Services = services;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetServicesByPlanResponse()
    {
    }
}

/// <summary>
/// Item de servicio en plan
/// </summary>
public class PlanServiceItem
{
    /// <summary>
    /// Id de la relación
    /// </summary>
    public int PlanServiceId { get; set; }

    /// <summary>
    /// Id del servicio
    /// </summary>
    public int ServiceId { get; set; }

    /// <summary>
    /// Nombre del servicio
    /// </summary>
    public string ServiceName { get; set; }

    /// <summary>
    /// Descripción del servicio
    /// </summary>
    public string ServiceDescription { get; set; }

    /// <summary>
    /// Requiere reserva
    /// </summary>
    public bool RequiresReservation { get; set; }
}

