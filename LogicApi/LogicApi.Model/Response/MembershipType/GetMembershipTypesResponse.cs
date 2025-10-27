namespace LogicApi.Model.Response.MembershipType;

/// <summary>
/// Respuesta de obtener tipos de membresía
/// </summary>
public class GetMembershipTypesResponse : IApiBaseResponse
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
    /// Lista de tipos de membresía
    /// </summary>
    public IEnumerable<MembershipTypeItem> MembershipTypes { get; set; }

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
    /// <param name="membershipTypes"></param>
    /// <param name="totalRecords"></param>
    /// <param name="currentPage"></param>
    /// <param name="pageSize"></param>
    public GetMembershipTypesResponse(IEnumerable<MembershipTypeItem> membershipTypes, int totalRecords, int currentPage, int pageSize)
    {
        MembershipTypes = membershipTypes;
        TotalRecords = totalRecords;
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetMembershipTypesResponse()
    {
    }
}

/// <summary>
/// Item de tipo de membresía
/// </summary>
public class MembershipTypeItem
{
    /// <summary>
    /// Guid del tipo de membresía
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre del gimnasio
    /// </summary>
    public string GymName { get; set; }

    /// <summary>
    /// Nombre del plan
    /// </summary>
    public string PlanName { get; set; }

    /// <summary>
    /// Precio del plan
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Duración en días
    /// </summary>
    public int DurationDays { get; set; }

    /// <summary>
    /// Descripción de beneficios
    /// </summary>
    public string BenefitsDescription { get; set; }

    /// <summary>
    /// Estado del tipo de membresía
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime? DateTimeRegister { get; set; }
}
