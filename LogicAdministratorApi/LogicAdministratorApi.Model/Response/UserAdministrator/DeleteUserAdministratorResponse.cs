namespace LogicAdministratorApi.Model.Response.UserAdministrator;

/// <summary>
/// Respuesta de eliminar usuario administrador
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="success"></param>
public class DeleteUserAdministratorResponse(bool success) : IApiBaseResponse
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
    /// Indica si la eliminación fue exitosa
    /// </summary>
    public bool Success { get; set; } = success;
}

