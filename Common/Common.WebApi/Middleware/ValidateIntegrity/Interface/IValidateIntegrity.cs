using Microsoft.AspNetCore.Http;

namespace Common.WebApi.Middleware.ValidateIntegrity.Interface;
/// <summary>
/// Interface para validar integridad
/// </summary>
public interface IValidateIntegrity
{
    /// <summary>
    /// Valida integridad
    /// </summary>
    /// <returns></returns>
    Task ValidateIntegrityAsync(HttpContext httpContext);
}