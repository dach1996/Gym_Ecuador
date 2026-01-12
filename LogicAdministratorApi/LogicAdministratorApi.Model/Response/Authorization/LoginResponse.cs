namespace LogicAdministratorApi.Model.Response.Authorization;
/// <summary>
/// Objeto de respuesta para ingreso de sistema
/// </summary>
public class LoginResponse : IApiBaseResponse
{
    /// <summary>
    /// Secreto para acceso
    /// </summary>
    public string AccessToken { get; set; }

    /// <summary>
    /// Username
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Person Name
    /// </summary>
    public string PersonName { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Is Super Admin
    /// </summary>
    public bool IsSuperAdmin { get; set; }

    /// <summary>
    /// Role Name
    /// </summary>
    public string RoleName { get; set; }

    /// <summary>
    /// Menu Items
    /// </summary>
    public IEnumerable<MenuItem> MenuItems { get; set; } = [];

    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mensaje
    /// </summary>
    /// <value></value>
    public bool ShowMessage { get; set; }
}

/// <summary>
/// Item de funcionalidad
/// </summary>
public class MenuItem
{
    /// <summary>
    /// Nombre de la funcionalidad
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Ruta de la funcionalidad
    /// </summary>
    public string Route { get; set; }

    /// <summary>
    /// Icono de la funcionalidad
    /// </summary>
    public string Icon { get; set; }
}