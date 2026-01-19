namespace LogicAdministratorApi.Model.Response.UserClient;

/// <summary>
/// Respuesta de obtener detalle de usuario cliente por GUID
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="client"></param>
public class GetUserClientByGuidResponse(PersonClientDetail client) : IApiBaseResponse
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
    /// Detalle del cliente
    /// </summary>
    public PersonClientDetail Client { get; set; } = client;
}

/// <summary>
/// Detalle completo de usuario cliente
/// </summary>
public class PersonClientDetail
{
    /// <summary>
    /// Guid del cliente (ClientGymBranch)
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Fecha de registro del cliente en la sucursal
    /// </summary>
    public DateTime RegistrationDate { get; set; }

    /// <summary>
    /// Estado del cliente en la sucursal
    /// </summary>
    public bool Status { get; set; }

    /// <summary>
    /// Guid del usuario
    /// </summary>
    public Guid UserGuid { get; set; }

    /// <summary>
    /// Nombre de usuario
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Email del usuario
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Teléfono del usuario
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Código de idioma
    /// </summary>
    public string LanguageCode { get; set; }

    /// <summary>
    /// Indica si el usuario está bloqueado
    /// </summary>
    public bool IsBlocked { get; set; }

    /// <summary>
    /// Indica si tiene registro completo
    /// </summary>
    public bool HasCompleteRegistration { get; set; }

    /// <summary>
    /// Fecha de registro del usuario
    /// </summary>
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Fecha del primer login
    /// </summary>
    public DateTime? FirstLoginDate { get; set; }

    /// <summary>
    /// URL de la imagen del usuario
    /// </summary>
    public string ImageUrl { get; set; }

    /// <summary>
    /// Guid de la persona
    /// </summary>
    public Guid PersonGuid { get; set; }

    /// <summary>
    /// Nombre de la persona
    /// </summary>
    public string PersonName { get; set; }

    /// <summary>
    /// Apellido de la persona
    /// </summary>
    public string PersonLastName { get; set; }

    /// <summary>
    /// Número de documento de la persona
    /// </summary>
    public string PersonDocumentNumber { get; set; }

    /// <summary>
    /// Fecha de nacimiento de la persona
    /// </summary>
    public DateTime? PersonBirthDate { get; set; }

    /// <summary>
    /// Guid de la sucursal de gimnasio
    /// </summary>
    public Guid GymBranchGuid { get; set; }

    /// <summary>
    /// Nombre de la sucursal de gimnasio
    /// </summary>
    public string GymBranchName { get; set; }

    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Nombre del gimnasio
    /// </summary>
    public string GymName { get; set; }

    /// <summary>
    /// Lista de membresías del cliente
    /// </summary>
    public List<ClientMembershipDetail> Memberships { get; set; } = new();
}

/// <summary>
/// Detalle de membresía del cliente
/// </summary>
public class ClientMembershipDetail
{
    /// <summary>
    /// Guid de la membresía
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Guid del plan de sucursal
    /// </summary>
    public Guid BranchPlanGuid { get; set; }

    /// <summary>
    /// Nombre del plan
    /// </summary>
    public string PlanName { get; set; }

    /// <summary>
    /// Fecha de inicio de la membresía
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de fin de la membresía
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Estado de la membresía (Activa/Inactiva)
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro de la membresía
    /// </summary>
    public DateTime RegistrationDate { get; set; }
}
