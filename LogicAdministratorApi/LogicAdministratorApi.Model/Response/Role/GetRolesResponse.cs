namespace LogicAdministratorApi.Model.Response.Role;

/// <summary>
/// Respuesta de obtener todos los roles
/// </summary>
public class GetRolesResponse : IApiBaseResponse
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
    /// Lista de roles
    /// </summary>
    public List<RoleItem> Roles { get; set; } = [];
}

/// <summary>
/// Item de rol
/// </summary>
public class RoleItem
{
    /// <summary>
    /// Guid del rol
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre del rol
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Descripción del rol
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Alcance del rol
    /// </summary>
    public string ScopeCode { get; set; }

    /// <summary>
    /// Nombre de la plataforma
    /// </summary>
    public string PlatformName { get; set; }
}
