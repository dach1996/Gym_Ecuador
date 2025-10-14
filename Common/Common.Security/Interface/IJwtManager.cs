

using Common.Security.Model;
using System.Security.Claims;
namespace Common.Security.Interface;
public interface IJwtManager
{
    /// <summary>
    /// Refresca el token JWT
    /// </summary>
    /// <param name="claims"></param>
    /// <param name="jwtConfigurationModel"></param>
    /// <returns></returns>
    JsonWebTokenModel RefreshJwt(Dictionary<string, string> claims);

    /// <summary>
    ///  Construye un token JWT
    /// </summary>
    /// <param name="claims"></param>
    /// <param name="jwtConfigurationModel"></param>
    /// <returns></returns>
    JsonWebTokenModel BuildJwt(Dictionary<string, string> claims);

    /// <summary>
    ///  Valida el token JWT
    /// </summary>
    /// <param name="jwt"></param>
    /// <param name="jwtConfigurationModel"></param>
    /// <returns></returns>
    ClaimsPrincipal ValidateJwt(string jwt);
}
