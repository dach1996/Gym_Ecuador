using LogicApi.Model.Response.PersonalGoal;

namespace LogicApi.Model.Request.PersonalGoal;

/// <summary>
/// Solicitud para actualizar un objetivo personal
/// </summary>
public class UpdatePersonalGoalRequest : IRequest<UpdatePersonalGoalResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del objetivo personal
    /// </summary>
    public Guid PersonalGoalGuid { get; set; }

    /// <summary>
    /// Valor objetivo
    /// </summary>
    public decimal TargetValue { get; set; }

    /// <summary>
    /// Fecha de fin estimada
    /// </summary>
    public DateTime? EstimatedEndDate { get; set; }

    /// <summary>
    /// Estado del objetivo
    /// </summary>
    public string GoalStatus { get; set; } // Activo, Completado, Abandonado

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
    public UpdatePersonalGoalRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdatePersonalGoalRequest()
    {
    }
}
