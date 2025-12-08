namespace LogicAdministratorApi.Model.Response.Authorization;
/// <summary>
/// Objeto de respuesta para ingreso de sistema
/// </summary>
public class LoginResponse : IApiBaseResponse
{
    /// <summary>
    /// Secreto para acceso
    /// </summary>
    public string AccessSecret { get; set; }

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
