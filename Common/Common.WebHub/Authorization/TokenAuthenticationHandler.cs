using Common.PluginFactory.Interface;
using Common.Security.Interface;
using Common.Security.Model.Enum;
using Common.Utils.Extensions;
using Common.WebHub.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text.Encodings.Web;
namespace Common.WebHub.Authorization;
public class JwtAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IPluginFactory pluginFactory,
    AppSettingsWebSockets appSettingsWebSockets
        ) : AuthenticationHandler<AuthenticationSchemeOptions>(
        options,
        logger,
        encoder
            )
{
    private readonly string _aesSecret = appSettingsWebSockets.AesConfiguration.Keys.FirstValueOrDefault(AesConfiguration.AesImplementationName.ServerGeneral);
    private readonly IJwtManager _jwtManager = pluginFactory.GetPlugin<IJwtManager>(JwtIdentifier.Mobile.ToString());
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            var authorization = Request.Headers.FirstOrDefault(p => p.Key.Equals("Authorization", StringComparison.InvariantCultureIgnoreCase)).Value;
            if (string.IsNullOrEmpty(authorization))
                return Task.FromResult(AuthenticateResult.Fail("No se encuentra Token en los Header: 'Authorization'"));
            var claimsPrincipal = _jwtManager.ValidateJwt(authorization);
            var principal = claimsPrincipal;
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            // pass on the ticket to the middleware
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch (SecurityTokenExpiredException ex)
        {
            //  Logger.LogWarning("Petición al servicio: '{@ServiceName}' y Device: '{@Device}' con Token Expirado a las: '{@ExpiredDate}'", Context.Request.Path.Value, device.Value, ex.Expires);
            return Task.FromResult(AuthenticateResult.Fail(ex));
        }
        catch (ArgumentException ex)
        {
            // Logger.LogWarning("Petición al servicio: '{@ServiceName}' y Device: '{@Device}' con Token no válido: {@Message}", Context.Request.Path.Value, device.Value, ex.Message);
            return Task.FromResult(AuthenticateResult.Fail(ex));
        }
        catch (Exception ex)
        {
            return Task.FromResult(AuthenticateResult.Fail(ex));
        }
    }
}
