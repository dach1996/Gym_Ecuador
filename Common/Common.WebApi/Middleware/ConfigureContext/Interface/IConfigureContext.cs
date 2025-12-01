

using Common.WebCommon.Models;
using Microsoft.AspNetCore.Http;

namespace Common.WebApi.Middleware.ConfigureContext.Interface;
/// <summary>
/// Interfáz de Configuración de contexto
/// </summary>
public interface IConfigureContext
{
    /// <summary>
    /// Valida el Contexto
    /// </summary>
    /// <returns></returns>
    CommonContextRequest ValidateContext(HttpContext httpContext);
}