using Common.Authentication.Models.Request;
using Common.Authentication.Models.Response;

namespace Common.Authentication;
/// <summary>
/// Servicios de Autenticaciones
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Autenticar 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
}