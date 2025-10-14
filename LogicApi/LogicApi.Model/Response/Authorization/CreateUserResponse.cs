namespace LogicApi.Model.Response.Authorization;
/// <summary>
/// Objeto de respuesta para ingreso de sistema
/// </summary>
public class CreateUserResponse : IApiBaseResponse
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
