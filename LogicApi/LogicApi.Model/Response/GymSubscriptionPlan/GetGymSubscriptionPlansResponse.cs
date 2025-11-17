namespace LogicApi.Model.Response.GymSubscriptionPlan;

/// <summary>
/// Respuesta de obtener planes de suscripción
/// </summary>
public class GetGymSubscriptionPlansResponse : IApiBaseResponse
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
    /// Lista de planes de suscripción
    /// </summary>
    public IEnumerable<GymSubscriptionPlanItem> Plans { get; set; }

    /// <summary>
    /// Total de registros
    /// </summary>
    public int TotalRecords { get; set; }

    /// <summary>
    /// Página actual
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Total de páginas
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="plans"></param>
    /// <param name="totalRecords"></param>
    /// <param name="currentPage"></param>
    /// <param name="pageSize"></param>
    public GetGymSubscriptionPlansResponse(IEnumerable<GymSubscriptionPlanItem> plans, int totalRecords, int currentPage, int pageSize)
    {
        Plans = plans;
        TotalRecords = totalRecords;
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetGymSubscriptionPlansResponse()
    {
    }
}

/// <summary>
/// Item de plan de suscripción
/// </summary>
public class GymSubscriptionPlanItem
{
    /// <summary>
    /// Guid del plan
    /// </summary>
    public Guid Guid { get; set; }

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

    /// <summary>
    /// Nombre del gimnasio
    /// </summary>
    public string GymName { get; set; }
}

