namespace Common.Authentication.Models.Request;
/// <summary>
/// Request para autenticar
/// </summary>
public class AuthenticationRequest
{
    /// <summary>
    /// Token de Autenticación 
    /// </summary>
    /// <value></value>
    public string Token { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="token"></param>
    public AuthenticationRequest(string token) => Token = token;
}