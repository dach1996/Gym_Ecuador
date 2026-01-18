namespace LogicAdministratorApi.Model.Response.UserAdministrator;

/// <summary>
/// Respuesta de obtener usuarios administradores paginados
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="totalRegister"></param>
/// <param name="registers"></param>
public class GetUsersAdministratorResponse(int totalRegister, IEnumerable<AdministratorUserItem> registers) : IPaginatorApiResponse<AdministratorUserItem>
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
    public IEnumerable<AdministratorUserItem> Registers { get; set; } = registers;
}

/// <summary>
/// Item de usuario administrador
/// </summary>
public class AdministratorUserItem
{

    /// <summary>
    /// Id del usuario
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Nombre completo de la persona
    /// </summary>
    public string PersonName { get; set; }

    /// <summary>
    /// Guid del usuario
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre de usuario
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Email del usuario
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Indica si el usuario está bloqueado
    /// </summary>
    public bool IsBlocked { get; set; }

    /// <summary>
    /// Teléfono del usuario
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Roles de usuario
    /// </summary>
    public List<AdministratorUserRoleScopeItem> UserRoleScopes { get; set; }
}
/// <summary>
/// Item de rol de usuario
/// </summary>
public class AdministratorUserRoleScopeItem
{
    /// <summary>
    /// Guid del rol
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre del rol
    /// </summary>
    public string Name { get; set; }
}