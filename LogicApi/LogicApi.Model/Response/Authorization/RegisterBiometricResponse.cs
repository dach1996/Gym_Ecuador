namespace LogicApi.Model.Response.Authorization;
/// <summary>
/// Objeto de respuesta para biométrico generado
/// </summary>
public class RegisterBiometricResponse : IApiBaseResponse
{
    /// <summary>
    /// Biométrico Generado
    /// </summary>
    /// <value></value>
    public string Biometric { get; set; }

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