namespace LogicAdministratorApi.Model.Response.UserClient;

/// <summary>
/// Respuesta de obtener usuarios clientes paginados
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="totalRegister"></param>
/// <param name="registers"></param>
public class GetUsersClientResponse(int totalRegister, IEnumerable<ClientItem> registers) : IPaginatorApiResponse<ClientItem>
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
    /// Total de registros
    /// </summary>
    public int TotalRegister { get; set; } = totalRegister;

    /// <summary>
    /// Registros
    /// </summary>
    public IEnumerable<ClientItem> Registers { get; set; } = registers;
}

/// <summary>
/// Item de usuario cliente
/// </summary>
public class ClientItem
{
    /// <summary>
    /// Guid del cliente (ClientGymBranch)
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Guid del usuario
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre completo de la persona
    /// </summary>
    public string PersonName { get; set; }

    /// <summary>
    /// Email del usuario
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Teléfono del usuario
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Lista de membresías del cliente
    /// </summary>
    /// <value></value>
    public List<ClientMembershipItem> MembershipsItems { get; set; } = [];
}

/// <summary>
/// Item de membresía del cliente
/// </summary>
public class ClientMembershipItem
{
    /// <summary>
    /// Nombre del gimnasio
    /// </summary>
    public string GymName { get; set; }

    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    public Guid GymGuid { get; set; }
    /// <summary>
    /// Nombre de la sucursal
    /// </summary>
    public string GymBranchName { get; set; }
    /// <summary>
    /// Guid de la sucursal
    /// </summary>
    public Guid GymBranchGuid { get; set; }
    /// <summary>
    /// Estado de la membresía
    /// </summary>
    public bool Status { get; set; }

    /// <summary>
    /// Fecha de registro de la membresía
    /// </summary>
    public DateTime RegistrationDate { get; set; }

    /// <summary>
    /// Fecha de registro de la membresía
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de fin de la membresía
    /// </summary>
    public DateTime? EndDate { get; set; }
}