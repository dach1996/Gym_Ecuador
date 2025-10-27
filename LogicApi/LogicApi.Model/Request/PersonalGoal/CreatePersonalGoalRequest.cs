using LogicApi.Model.Response.PersonalGoal;

namespace LogicApi.Model.Request.PersonalGoal;

/// <summary>
/// Solicitud para crear un objetivo personal
/// </summary>
public class CreatePersonalGoalRequest : IRequest<CreatePersonalGoalResponse>, IApiBaseRequest
{
    /// <summary>
    /// Id de la persona
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// Tipo de objetivo
    /// </summary>
    public string GoalType { get; set; } // Perder peso, Ganar músculo, Aumentar fuerza

    /// <summary>
    /// Valor inicial
    /// </summary>
    public decimal? InitialValue { get; set; }

    /// <summary>
    /// Valor objetivo
    /// </summary>
    public decimal TargetValue { get; set; }

    /// <summary>
    /// Fecha de inicio del objetivo
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de fin estimada
    /// </summary>
    public DateTime? EstimatedEndDate { get; set; }

    /// <summary>
    /// Descripción del objetivo
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public CreatePersonalGoalRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreatePersonalGoalRequest()
    {
    }
}
