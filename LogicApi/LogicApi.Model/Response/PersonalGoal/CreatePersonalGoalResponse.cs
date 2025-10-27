namespace LogicApi.Model.Response.PersonalGoal;

/// <summary>
/// Respuesta de crear objetivo personal
/// </summary>
public class CreatePersonalGoalResponse : IApiBaseResponse
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
    /// Guid del objetivo personal creado
    /// </summary>
    public Guid PersonalGoalGuid { get; set; }

    /// <summary>
    /// Tipo de objetivo
    /// </summary>
    public string GoalType { get; set; }

    /// <summary>
    /// Valor objetivo
    /// </summary>
    public decimal TargetValue { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="personalGoalGuid"></param>
    /// <param name="goalType"></param>
    /// <param name="targetValue"></param>
    public CreatePersonalGoalResponse(Guid personalGoalGuid, string goalType, decimal targetValue)
    {
        PersonalGoalGuid = personalGoalGuid;
        GoalType = goalType;
        TargetValue = targetValue;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreatePersonalGoalResponse()
    {
    }
}
