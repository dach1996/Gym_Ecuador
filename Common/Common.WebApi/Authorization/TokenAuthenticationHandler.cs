using Common.PluginFactory.Interface;
using Common.Security.Interface;
using Common.Security.Model.Enum;
using Common.WebApi.Models.Enum;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text.Encodings.Web;

namespace Common.WebApi.Authorization;
public class TokenAuthenticationHandler(
    IOptionsMonitor<CustomAuthOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IPluginFactory pluginFactory
        ) : AuthenticationHandler<CustomAuthOptions>(
        options,
        logger,
        encoder
            )
{
    private readonly IJwtManager _jwtManager = pluginFactory.GetPlugin<IJwtManager>($"{JwtIdentifier.Mobile}");

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var device = Request.Headers.FirstOrDefault(p => p.Key.Equals("X-Device", StringComparison.InvariantCultureIgnoreCase));
        try
        {
            var channel = Request.Headers.FirstOrDefault(p => p.Key.Equals("X-Channel", StringComparison.InvariantCultureIgnoreCase));
            if (!Enum.TryParse<Channel>(channel.Value, out var _))
                return Task.FromResult(AuthenticateResult.Fail("No se encuentra Header: 'X-Channel'"));
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
            Logger.LogWarning("Petición al servicio: '{@ServiceName}' y Device: '{@Device}' con Token Expirado a las: '{@ExpiredDate}'", Context.Request.Path.Value, device.Value, ex.Expires);
            return Task.FromResult(AuthenticateResult.Fail(ex));
        }
        catch (ArgumentException ex)
        {
            Logger.LogWarning("Petición al servicio: '{@ServiceName}' y Device: '{@Device}' con Token no válido: {@Message}", Context.Request.Path.Value, device.Value, ex.Message);
            return Task.FromResult(AuthenticateResult.Fail(ex));
        }
        catch (Exception ex)
        {
            return Task.FromResult(AuthenticateResult.Fail(ex));
        }
    }
}

/// <summary>
/// Opciones
/// </summary>
public class CustomAuthOptions : AuthenticationSchemeOptions
{
    public int Id { get; set; }
}

/// <summary>
/// Schema
/// </summary>
public static class SchemaName
{
    public const string CustomSchema = "CustomSchema";
}

