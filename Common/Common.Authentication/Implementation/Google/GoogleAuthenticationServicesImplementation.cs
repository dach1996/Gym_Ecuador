using Common.Authentication.AuthenticationServicesException;
using Common.Authentication.Models.Configuration;
using Common.Authentication.Models.Request;
using Common.Authentication.Models.Response;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Common.Authentication.Implementation.Google;
/// <summary>
/// Implementación de Google services
/// /// </summary>
public class GoogleAuthenticationServicesImplementation : AuthenticationServicesBase, IAuthenticationService
{
    protected override AuthenticationImplementationName ImplementationName => AuthenticationImplementationName.Google;
    protected readonly GoogleConfiguration Configuration;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public GoogleAuthenticationServicesImplementation(
        ILogger<GoogleAuthenticationServicesImplementation> logger,
        IConfiguration configuration
        ) : base(logger)
    {
        Configuration = configuration.GetSection(nameof(AuthenticationServiceConfiguration)).Get<AuthenticationServiceConfiguration<GoogleConfiguration>>()
          ?.Implementations?.FirstOrDefault(where => where.Identifier == $"{ImplementationName}")?.Information
           ?? throw new CustomAuthenticationServicesException($"No se encontró la configuración de servicios de authenticación con identificador{ImplementationName}");
    }

    /// <summary>
    /// Autenticación 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
    {
        try
        {
            //configura las validaciones
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = Configuration.Audiences
            };
            //Validar Payload
            var validPayload = await GoogleJsonWebSignature.ValidateAsync(request.Token, settings).ConfigureAwait(false);
            //Imagen
            return new(validPayload.Email, validPayload.Name, validPayload.Picture);
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