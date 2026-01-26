using LogicCommon.Model.Response.File;

namespace LogicAdministratorApi.Model.Response.ClientMembership;

/// <summary>
/// Respuesta de obtener membresías de clientes paginadas
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
public class GetClientMembershipsResponse : IApiBaseResponse
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
    /// Person full name
    /// </summary>
    public string PersonName { get; set; }
    /// <summary>
    /// Person document number
    /// </summary>
    public string PersonDocumentNumber { get; set; }

    /// <summary>
    /// Person birth date
    /// </summary>
    public DateTime? PersonBirthDate { get; set; }
    
    /// <summary>
    /// Email of the user
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Phone of the user
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Status of the client membership
    /// </summary>
    public bool Status { get; set; }

    /// <summary>
    /// First subscription date
    /// </summary>
    public DateTime FirstSubscriptionDate { get; set; }

    /// <summary>
    /// Registros
    /// </summary>
    public IEnumerable<CurrentSubscription> Registers { get; set; } = [];
}

/// <summary>
/// Datos de la suscripción actual
/// </summary>
public class CurrentSubscription
{
    /// <summary>
    /// Guid de la membresía
    /// </summary>
    public Guid MembershipGuid { get; set; }

    /// <summary>
    /// Fecha de inicio del periodo vigente
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de fin del periodo vigente
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Estado de la suscripción (activo/inactivo)
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Nombre del plan
    /// </summary>
    public string PlanName { get; set; }

    /// <summary>
    /// Valor de la suscripción
    /// </summary>
    public decimal SubscriptionValue { get; set; }

    /// <summary>
    /// Método de pago
    /// </summary>
    public string PaymentMethod { get; set; }

    /// <summary>
    /// Foto de la suscripción (comprobante o imagen relacionada)
    /// </summary>
    public FileUrlResponse SubscriptionPhoto { get; set; }
}
