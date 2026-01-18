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
    /// Indica si el usuario está bloqueado
    /// </summary>
    public bool IsBlocked { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime DateTimeRegister { get; set; }
}

