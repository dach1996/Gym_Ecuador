using LogicApi.Model.Response.PersonalGoal;

namespace LogicApi.Model.Request.PersonalGoal;

/// <summary>
/// Solicitud para obtener objetivos personales
/// </summary>
public class GetPersonalGoalsRequest : IRequest<GetPersonalGoalsResponse>, IApiBaseRequest
{
    /// <summary>
    /// Id de la persona (filtro opcional)
    /// </summary>
    public int? PersonId { get; set; }

    /// <summary>
    /// Filtro por tipo de objetivo
    /// </summary>
    public string GoalTypeFilter { get; set; }

    /// <summary>
    /// Filtro por estado del objetivo
    /// </summary>
    public string GoalStatusFilter { get; set; }

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
    public GetPersonalGoalsRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetPersonalGoalsRequest()
    {
    }
}
