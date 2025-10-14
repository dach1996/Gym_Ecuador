using Common.Authentication.AuthenticationServicesException;
using Common.Authentication.Models.Request;
using Common.Authentication.Models.Response;
using Microsoft.Extensions.Logging;

namespace Common.Authentication.Implementation.Mock;
/// <summary>
/// Implementación de Google services
/// /// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="logger"></param>
/// <param name="configuration"></param>
/// <returns></returns>
public class MockAuthenticationServicesImplementation(
    ILogger<MockAuthenticationServicesImplementation> logger
        ) : AuthenticationServicesBase(logger), IAuthenticationService
{
    protected override AuthenticationImplementationName ImplementationName => AuthenticationImplementationName.Google;

    /// <summary>
    /// Autenticación 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
    {
        try
        {
            return await Task.FromResult(new AuthenticationResponse("pedrodhidalgo4315@gmail.com", "Pedro Hidalgo", "")).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al validar autenticación por: " +
               "Implementación: '{@Implementation}' - " +
               "Message: '{@Message}'", AuthenticationImplementationName.Google, ex.Message);
            throw new CustomAuthenticationServicesException(ex.Message);
        }
    }
}