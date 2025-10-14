namespace LogicApi.Model.Response.Authorization;
/// <summary>
/// Objeto de respuesta resetear contraseña
/// </summary>
public class PasswordForgottenResponse : IApiBaseResponse
{
    /// <summary>
    /// Contraseña temporal
    /// </summary>
    /// <value></value>
    public string TemporalPassword { get; set; }

    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    /// <value></value>
    public bool ShowMessage { get; set; }
}