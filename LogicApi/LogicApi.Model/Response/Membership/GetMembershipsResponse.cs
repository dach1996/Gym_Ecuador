namespace LogicApi.Model.Response.Membership;

/// <summary>
/// Respuesta de obtener membresías
/// </summary>
public class GetMembershipsResponse : IApiBaseResponse
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
    /// Lista de membresías
    /// </summary>
    public IEnumerable<MembershipItem> Memberships { get; set; }

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
    /// <param name="memberships"></param>
    /// <param name="totalRecords"></param>
    /// <param name="currentPage"></param>
    /// <param name="pageSize"></param>
    public GetMembershipsResponse(IEnumerable<MembershipItem> memberships, int totalRecords, int currentPage, int pageSize)
    {
        Memberships = memberships;
        TotalRecords = totalRecords;
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetMembershipsResponse()
    {
    }
}

/// <summary>
/// Item de membresía
/// </summary>
public class MembershipItem
{
    /// <summary>
    /// Guid de la membresía
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre completo de la persona
    /// </summary>
    public string PersonFullName { get; set; }

    /// <summary>
    /// Nombre del gimnasio
    /// </summary>
    public string GymName { get; set; }

    /// <summary>
    /// Nombre del tipo de membresía
    /// </summary>
    public string MembershipTypeName { get; set; }

    /// <summary>
    /// Fecha de inicio
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de fin
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Estado de la membresía
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Rol en el gimnasio
    /// </summary>
    public string GymRole { get; set; }

    /// <summary>
    /// Días restantes
    /// </summary>
    public int DaysRemaining { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime? DateTimeRegister { get; set; }
}
