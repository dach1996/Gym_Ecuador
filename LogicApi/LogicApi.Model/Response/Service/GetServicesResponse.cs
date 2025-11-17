namespace LogicApi.Model.Response.Service;

/// <summary>
/// Respuesta de obtener servicios
/// </summary>
public class GetServicesResponse : IApiBaseResponse
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
    /// Lista de servicios
    /// </summary>
    public IEnumerable<ServiceItem> Services { get; set; }

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
    /// <param name="services"></param>
    /// <param name="totalRecords"></param>
    /// <param name="currentPage"></param>
    /// <param name="pageSize"></param>
    public GetServicesResponse(IEnumerable<ServiceItem> services, int totalRecords, int currentPage, int pageSize)
    {
        Services = services;
        TotalRecords = totalRecords;
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetServicesResponse()
    {
    }
}

/// <summary>
/// Item de servicio
/// </summary>
public class ServiceItem
{
    /// <summary>
    /// Id del servicio
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del servicio
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Descripción del servicio
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Requiere reserva previa
    /// </summary>
    public bool RequiresReservation { get; set; }

    /// <summary>
    /// Estado activo
    /// </summary>
    public bool IsActive { get; set; }
}

