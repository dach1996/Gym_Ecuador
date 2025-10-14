using Common.Security.Interface;
using Common.Security.Model;
using Common.Security.Model.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Common.Security.Implementation;
public abstract class JwtManagerBase : IJwtManager
{
    protected abstract JwtIdentifier JwtIdentifier { get; }

    protected readonly JwtSettings JwtConfigurationModel;

    protected JwtManagerBase(IConfiguration configuration)
    {
        JwtConfigurationModel = configuration.GetSection(nameof(JwtSettings)).Get<List<JwtSettings>>()
         ?.Find(where => where.Identifier == JwtIdentifier)
          ?? throw new InvalidOperationException($"No se encontró la configuración de Jwt con identificador: {JwtIdentifier}");
    }

    /// <summary>
    /// Refresca el token JWT
    /// </summary>
    /// <param name="claims"></param>
    /// <returns></returns>
    public JsonWebTokenModel RefreshJwt(Dictionary<string, string> claims)
    => BuildJwt(claims);

    /// <summary>
    ///  Construye un token JWT
    /// </summary>
    /// <param name="claims"></param>
    /// <returns></returns>
    public JsonWebTokenModel BuildJwt(Dictionary<string, string> claims)
    {
        var accessTokenExpiration = DateTime.UtcNow.AddSeconds(JwtConfigurationModel.AccessExpiration);

        IdentityModelEventSource.ShowPII = true;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfigurationModel.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claimsToken = BuildClaims(claims);

        var jwtToken = new JwtSecurityToken(
            JwtConfigurationModel.Issuer,
            JwtConfigurationModel.Audience,
            claimsToken,
            expires: DateTime.Now.AddMinutes(JwtConfigurationModel.AccessExpiration),
            signingCredentials: credentials
        );

        var handler = new JwtSecurityTokenHandler();
        var accessToken = handler.WriteToken(jwtToken);

        return new JsonWebTokenModel(accessToken, accessTokenExpiration.Ticks);
    }

    /// <summary>
    ///  Valida el token JWT
    /// </summary>
    /// <param name="jwt"></param>
    /// <returns></returns>
    public ClaimsPrincipal ValidateJwt(string jwt)
    {
        //Toma la configuración de la autenticación
        byte[] secret = Encoding.ASCII.GetBytes(JwtConfigurationModel.Secret);

        //Arma los parámetros
        var validationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.FromMinutes(JwtConfigurationModel.AccessExpiration),
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secret),
            ValidIssuer = JwtConfigurationModel.Issuer,
            ValidAudience = JwtConfigurationModel.Audience,
            ValidateIssuer = true,
            ValidateAudience = true
        };

        //Valida el token JWT
        var jwtClean = jwt.Replace("bearer ", "", StringComparison.InvariantCultureIgnoreCase);
        return new JwtSecurityTokenHandler().ValidateToken(jwtClean, validationParameters, out var _);
    }

    private static IEnumerable<Claim> BuildClaims(Dictionary<string, string> claims)
        => claims.Select(t => new Claim(t.Key, t.Value));

}
