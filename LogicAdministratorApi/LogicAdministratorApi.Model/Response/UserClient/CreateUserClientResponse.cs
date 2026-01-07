namespace LogicAdministratorApi.Model.Response.UserClient;

/// <summary>
/// Respuesta de crear usuario cliente
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="userGuid"></param>
/// <param name="userName"></param>
/// <param name="email"></param>
public class CreateUserClientResponse(Guid userGuid, string userName, string email) : IApiBaseResponse
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
    /// Guid del usuario creado
    /// </summary>
    public Guid UserGuid { get; set; } = userGuid;

    /// <summary>
    /// Nombre de usuario
    /// </summary>
    public string UserName { get; set; } = userName;

    /// <summary>
    /// Email del usuario
    /// </summary>
    public string Email { get; set; } = email;
}

