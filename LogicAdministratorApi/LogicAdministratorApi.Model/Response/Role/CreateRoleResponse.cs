namespace LogicAdministratorApi.Model.Response.Role;

/// <summary>
/// Respuesta de crear un nuevo rol
/// </summary>
public class CreateRoleResponse : IApiBaseResponse
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
    /// Guid del rol creado
    /// </summary>
    public Guid RoleGuid { get; set; }

    /// <summary>
    /// Nombre del rol creado
    /// </summary>
    public string Name { get; set; }
}
