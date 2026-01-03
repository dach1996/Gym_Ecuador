namespace LogicAdministratorApi.Model.Response.UserAdministrator;

/// <summary>
/// Respuesta de actualizar usuario administrador
/// </summary>
public class UpdateUserAdministratorResponse : IApiBaseResponse
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
    /// Guid del usuario actualizado
    /// </summary>
    public Guid UserGuid { get; set; }

    /// <summary>
    /// Nombre de usuario
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Email del usuario
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userGuid"></param>
    /// <param name="userName"></param>
    /// <param name="email"></param>
    public UpdateUserAdministratorResponse(Guid userGuid, string userName, string email)
    {
        UserGuid = userGuid;
        UserName = userName;
        Email = email;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateUserAdministratorResponse()
    {
    }
}

