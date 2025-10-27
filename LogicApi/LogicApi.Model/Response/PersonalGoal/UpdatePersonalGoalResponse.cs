namespace LogicApi.Model.Response.PersonalGoal;

/// <summary>
/// Respuesta de actualizar objetivo personal
/// </summary>
public class UpdatePersonalGoalResponse : IApiBaseResponse
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
    /// Guid del objetivo personal actualizado
    /// </summary>
    public Guid PersonalGoalGuid { get; set; }

    /// <summary>
    /// Estado del objetivo
    /// </summary>
    public string GoalStatus { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="personalGoalGuid"></param>
    /// <param name="goalStatus"></param>
    public UpdatePersonalGoalResponse(Guid personalGoalGuid, string goalStatus)
    {
        PersonalGoalGuid = personalGoalGuid;
        GoalStatus = goalStatus;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdatePersonalGoalResponse()
    {
    }
}
