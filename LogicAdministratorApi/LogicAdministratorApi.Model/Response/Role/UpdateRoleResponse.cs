namespace LogicAdministratorApi.Model.Response.Role;

/// <summary>
/// Respuesta de actualizar un rol
/// </summary>
public class UpdateRoleResponse : IApiBaseResponse
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
    /// Guid del rol actualizado
    /// </summary>
    public Guid RoleGuid { get; set; }

    /// <summary>
    /// Nombre del rol actualizado
    /// </summary>
    public string Name { get; set; }
}
