using Common.PluginFactory.Interface;
using Common.WebApi.Middleware.ConfigureContext.Interface;
using Common.WebCommon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Common.WebApi.Middleware.ConfigureContext.Implementations;
/// <summary>
/// Clase base para Middlerae
/// </summary>
public abstract class ConfigureContextBase(ILogger<ConfigureContextBase> logger, IPluginFactory pluginFactory, AppSettingsCommon appSettings)
     : MiddlewareImplementationBase(logger, pluginFactory, appSettings), IConfigureContext
{

    /// <summary>
    /// Valida el Contexto
    /// </summary>
    /// <returns></returns>
    public abstract CommonContextRequest ValidateContext(HttpContext httpContext);
}