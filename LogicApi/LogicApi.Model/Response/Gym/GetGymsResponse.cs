namespace LogicApi.Model.Response.Gym;

/// <summary>
/// Respuesta de obtener gimnasios
/// </summary>
public class GetGymsResponse : IApiBaseResponse
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
    /// Lista de gimnasios
    /// </summary>
    public IEnumerable<GymItem> Gyms { get; set; }

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
    /// <param name="gyms"></param>
    /// <param name="totalRecords"></param>
    /// <param name="currentPage"></param>
    /// <param name="pageSize"></param>
    public GetGymsResponse(IEnumerable<GymItem> gyms, int totalRecords, int currentPage, int pageSize)
    {
        Gyms = gyms;
        TotalRecords = totalRecords;
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetGymsResponse()
    {
    }
}

/// <summary>
/// Item de gimnasio
/// </summary>
public class GymItem
{
    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre del gimnasio
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Descripción corta del gimnasio
    /// </summary>
    public string ShortDescription { get; set; }

    /// <summary>
    /// Dirección del gimnasio
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Teléfono del gimnasio
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Email del gimnasio
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Horario de apertura
    /// </summary>
    public TimeSpan? OpeningTime { get; set; }

    /// <summary>
    /// Horario de cierre
    /// </summary>
    public TimeSpan? ClosingTime { get; set; }

    /// <summary>
    /// Estado del gimnasio
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime? DateTimeRegister { get; set; }
}
