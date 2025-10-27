namespace LogicApi.Model.Response.PersonalGoal;

/// <summary>
/// Respuesta de obtener objetivos personales
/// </summary>
public class GetPersonalGoalsResponse : IApiBaseResponse
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
    /// Lista de objetivos personales
    /// </summary>
    public IEnumerable<PersonalGoalItem> PersonalGoals { get; set; }

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
    /// <param name="personalGoals"></param>
    /// <param name="totalRecords"></param>
    /// <param name="currentPage"></param>
    /// <param name="pageSize"></param>
    public GetPersonalGoalsResponse(IEnumerable<PersonalGoalItem> personalGoals, int totalRecords, int currentPage, int pageSize)
    {
        PersonalGoals = personalGoals;
        TotalRecords = totalRecords;
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetPersonalGoalsResponse()
    {
    }
}

/// <summary>
/// Item de objetivo personal
/// </summary>
public class PersonalGoalItem
{
    /// <summary>
    /// Guid del objetivo personal
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre completo de la persona
    /// </summary>
    public string PersonFullName { get; set; }

    /// <summary>
    /// Tipo de objetivo
    /// </summary>
    public string GoalType { get; set; }

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
    /// Estado del objetivo
    /// </summary>
    public string GoalStatus { get; set; }

    /// <summary>
    /// Progreso (porcentaje)
    /// </summary>
    public decimal ProgressPercentage { get; set; }

    /// <summary>
    /// Descripción del objetivo
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime? DateTimeRegister { get; set; }
}
