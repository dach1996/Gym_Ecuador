namespace LogicAdministratorApi.Model.Response.Role;

/// <summary>
/// Respuesta de obtener detalle de rol por GUID
/// </summary>
public class GetRoleDetailResponse(RoleDetail role) : IApiBaseResponse
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
    /// Datos del rol
    /// </summary>
    public RoleDetail Role { get; set; } = role;
}

/// <summary>
/// Detalle completo de rol
/// </summary>
public class RoleDetail
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

    /// <summary>
    /// Lista de GUIDs de las funcionalidades asignadas al rol
    /// </summary>
    public List<Guid> FunctionalityGuids { get; set; } = new();
}
