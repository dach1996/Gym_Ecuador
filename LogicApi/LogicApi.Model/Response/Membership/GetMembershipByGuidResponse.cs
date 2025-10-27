namespace LogicApi.Model.Response.Membership;

/// <summary>
/// Respuesta de obtener membresía por GUID
/// </summary>
public class GetMembershipByGuidResponse : IApiBaseResponse
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
    /// Datos de la membresía
    /// </summary>
    public MembershipDetail Membership { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="membership"></param>
    public GetMembershipByGuidResponse(MembershipDetail membership)
    {
        Membership = membership;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetMembershipByGuidResponse()
    {
    }
}

/// <summary>
/// Detalle completo de membresía
/// </summary>
public class MembershipDetail
{
    /// <summary>
    /// Guid de la membresía
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Información de la persona
    /// </summary>
    public MembershipPersonInfo Person { get; set; }

    /// <summary>
    /// Información del gimnasio
    /// </summary>
    public MembershipGymInfo Gym { get; set; }

    /// <summary>
    /// Información del tipo de membresía
    /// </summary>
    public MembershipTypeInfo MembershipType { get; set; }

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

/// <summary>
/// Información de persona para membresía
/// </summary>
public class MembershipPersonInfo
{
    /// <summary>
    /// Nombre completo
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Teléfono
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Documento de identidad
    /// </summary>
    public string DocumentNumber { get; set; }
}

/// <summary>
/// Información de gimnasio para membresía
/// </summary>
public class MembershipGymInfo
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
    /// Dirección del gimnasio
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Teléfono del gimnasio
    /// </summary>
    public string Phone { get; set; }
}

/// <summary>
/// Información de tipo de membresía
/// </summary>
public class MembershipTypeInfo
{
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
}
