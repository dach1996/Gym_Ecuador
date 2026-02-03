namespace LogicApi.Model.Response.ClientMembership;

/// <summary>
/// Respuesta de obtener mis membresías agrupadas por sucursal
/// </summary>
public class GetMyMembershipsResponse(List<GymBranchMembershipGroup> gymBranchMemberships) : IApiBaseResponse
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
    /// Membresías agrupadas por sucursal de gimnasio
    /// </summary>
    public List<GymBranchMembershipGroup> GymBranchMemberships { get; set; } = gymBranchMemberships;
}

/// <summary>
/// Grupo de membresías por sucursal de gimnasio
/// </summary>
public class GymBranchMembershipGroup
{
    /// <summary>
    /// GUID de la sucursal
    /// </summary>
    public Guid GymBranchGuid { get; set; }

    /// <summary>
    /// Nombre de la sucursal
    /// </summary>
    public string GymBranchName { get; set; }

    /// <summary>
    /// URL de imagen de la sucursal
    /// </summary>
    public string GymBranchImageUrl { get; set; }

    /// <summary>
    /// Membresía activa actual (null si no tiene)
    /// </summary>
    public MembershipHistoryItem ActiveMembership { get; set; }

    /// <summary>
    /// Historial de membresías en esta sucursal
    /// </summary>
    public List<MembershipHistoryItem> MembershipHistory { get; set; }
}

/// <summary>
/// Item del historial de membresía
/// </summary>
public class MembershipHistoryItem
{
    /// <summary>
    /// GUID de la membresía
    /// </summary>
    public Guid MembershipGuid { get; set; }

    /// <summary>
    /// Nombre del plan
    /// </summary>
    public string PlanName { get; set; }

    /// <summary>
    /// Descripción del plan
    /// </summary>
    public string PlanDescription { get; set; }

    /// <summary>
    /// Precio del plan
    /// </summary>
    public decimal PlanPrice { get; set; }

    /// <summary>
    /// Fecha de inicio de la membresía
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de fin de la membresía
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Estado de la membresía
    /// </summary>
    public MembershipStatus Status { get; set; }
}

/// <summary>
/// Estado de la membresía
/// </summary>
public enum MembershipStatus
{
    /// <summary>
    /// Membresía activa
    /// </summary>
    Active = 1,

    /// <summary>
    /// Membresía expirada
    /// </summary>
    Expired = 2,

    /// <summary>
    /// Membresía cancelada
    /// </summary>
    Cancelled = 3
}
