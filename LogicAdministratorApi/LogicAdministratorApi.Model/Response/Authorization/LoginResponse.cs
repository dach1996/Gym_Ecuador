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
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Gym Guids
    /// </summary>
    public List<Guid> GymGuids { get; set; }

    /// <summary>
    /// Mensaje
    /// </summary>
    /// <value></value>
    public bool ShowMessage { get; set; }
}
