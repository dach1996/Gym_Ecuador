using LogicApi.Model.Response.GymSubscriptionPlan;

namespace LogicApi.Model.Request.GymSubscriptionPlan;

/// <summary>
/// Solicitud para obtener planes de suscripción
/// </summary>
public class GetGymSubscriptionPlansRequest : IRequest<GetGymSubscriptionPlansResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del gimnasio (opcional, para filtrar por gimnasio)
    /// </summary>
    public Guid? GymGuid { get; set; }

    /// <summary>
    /// Filtro por nombre
    /// </summary>
    public string NameFilter { get; set; }

    /// <summary>
    /// Filtro por estado activo
    /// </summary>
    public bool? IsActiveFilter { get; set; }

    /// <summary>
    /// Página
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Tamaño de página
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public GetGymSubscriptionPlansRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetGymSubscriptionPlansRequest()
    {
    }
}

