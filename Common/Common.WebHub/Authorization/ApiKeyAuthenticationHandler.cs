using Common.WebHub.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
namespace Common.WebHub.Authorization;
public class ApiKeyAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    AppSettingsWebSockets appSettingsWebSockets
        ) : AuthenticationHandler<AuthenticationSchemeOptions>(
        options,
        logger,
        encoder
            )
{

    const string API_KEY_HEADER = "X-Api-Key";

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            if (!Request.Headers.TryGetValue(API_KEY_HEADER, out var apiKey))
                return Task.FromResult(AuthenticateResult.Fail("No se Encontro el Header: 'Authorization'"));
            if (apiKey.ToString() != appSettingsWebSockets.ApiKey)
                return Task.FromResult(AuthenticateResult.Fail("Clave de API inválida"));
            var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, "Administrator")
                };

            var identity = new ClaimsIdentity(claims, "ApiKey");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "ApiKey");
            // pass on the ticket to the middleware
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch (Exception ex)
        {
            return Task.FromResult(AuthenticateResult.Fail(ex));
        }
    }
}
