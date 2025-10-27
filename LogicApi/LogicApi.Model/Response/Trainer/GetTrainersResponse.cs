namespace LogicApi.Model.Response.Trainer;

/// <summary>
/// Respuesta de obtener entrenadores
/// </summary>
public class GetTrainersResponse : IApiBaseResponse
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
    /// Lista de entrenadores
    /// </summary>
    public IEnumerable<TrainerItem> Trainers { get; set; }

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
    /// <param name="trainers"></param>
    /// <param name="totalRecords"></param>
    /// <param name="currentPage"></param>
    /// <param name="pageSize"></param>
    public GetTrainersResponse(IEnumerable<TrainerItem> trainers, int totalRecords, int currentPage, int pageSize)
    {
        Trainers = trainers;
        TotalRecords = totalRecords;
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetTrainersResponse()
    {
    }
}

/// <summary>
/// Item de entrenador
/// </summary>
public class TrainerItem
{
    /// <summary>
    /// Guid del entrenador
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre completo de la persona
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Especialidad del entrenador
    /// </summary>
    public string Specialty { get; set; }

    /// <summary>
    /// URL de foto de perfil
    /// </summary>
    public string ProfilePhotoUrl { get; set; }

    /// <summary>
    /// Nombre del gimnasio
    /// </summary>
    public string GymName { get; set; }

    /// <summary>
    /// Estado del entrenador
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime? DateTimeRegister { get; set; }
}
