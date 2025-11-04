namespace LogicApi.Model.Response.GymBranch;

/// <summary>
/// Respuesta de obtener sucursales de gimnasio
/// </summary>
public class GetGymBranchesResponse : IApiBaseResponse
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
    /// Lista de sucursales
    /// </summary>
    public IEnumerable<GymBranchItem> GymBranches { get; set; }

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
    /// <param name="gymBranches"></param>
    /// <param name="totalRecords"></param>
    /// <param name="currentPage"></param>
    /// <param name="pageSize"></param>
    public GetGymBranchesResponse(IEnumerable<GymBranchItem> gymBranches, int totalRecords, int currentPage, int pageSize)
    {
        GymBranches = gymBranches;
        TotalRecords = totalRecords;
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetGymBranchesResponse()
    {
    }
}

/// <summary>
/// Item de sucursal de gimnasio
/// </summary>
public class GymBranchItem
{
    /// <summary>
    /// Guid de la sucursal
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Guid del gimnasio principal
    /// </summary>
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Nombre del gimnasio principal
    /// </summary>
    public string GymName { get; set; }

    /// <summary>
    /// Nombre de la sucursal
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Código de la sucursal
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Descripción de la sucursal
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Dirección de la sucursal
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Teléfono de la sucursal
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Email de la sucursal
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Latitud
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// Longitud
    /// </summary>
    public decimal Longitude { get; set; }

    /// <summary>
    /// Capacidad máxima
    /// </summary>
    public int? MaxCapacity { get; set; }

    /// <summary>
    /// Área en metros cuadrados
    /// </summary>
    public decimal? AreaSquareMeters { get; set; }

    /// <summary>
    /// Número de pisos
    /// </summary>
    public byte? FloorCount { get; set; }

    /// <summary>
    /// Estado de la sucursal
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de apertura
    /// </summary>
    public DateTime? OpeningDate { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime DateTimeRegister { get; set; }
}

