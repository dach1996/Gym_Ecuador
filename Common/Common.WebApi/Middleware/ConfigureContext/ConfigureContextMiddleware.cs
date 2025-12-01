using Common.PluginFactory.Interface;
using Common.WebApi.Middleware.ConfigureContext.Interface;
using Common.WebCommon.Models;
using Microsoft.AspNetCore.Http;
namespace Common.WebApi.Middleware.ConfigureContext;
public class ConfigureContextMiddleware(
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
        httpContext.Items[nameof(CommonContextRequest)] = pluginFactory
            .GetPlugin<IConfigureContext>(appSettings.GeneralConfiguration.ConfigureContextImplementation.ToUpper(), true)
            .ValidateContext(httpContext);
        await _next(httpContext).ConfigureAwait(false);
    }
}
