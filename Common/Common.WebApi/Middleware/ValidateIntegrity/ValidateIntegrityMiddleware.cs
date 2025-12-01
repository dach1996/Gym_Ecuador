using Common.PluginFactory.Interface;
using Common.WebApi.Middleware.ValidateIntegrity.Interface;
using Common.WebCommon.Models;
using Microsoft.AspNetCore.Http;

namespace Common.WebApi.Middleware;
/// <summary>
/// Constructor
/// </summary>
/// <param name="next"></param>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
/// <returns></returns>
public class ValidateIntegrityMiddleware(
    RequestDelegate next,
    IPluginFactory pluginFactory,
    AppSettingsCommon appSettings)
{


    private readonly RequestDelegate _next = next;

    /// <summary>
    /// Invoke
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        await pluginFactory
            .GetPlugin<IValidateIntegrity>(appSettings.GeneralConfiguration.ValidateIntegrityImplementation.ToUpper(), true)
            .ValidateIntegrityAsync(httpContext).ConfigureAwait(false);
        await _next(httpContext).ConfigureAwait(false);
    }
}