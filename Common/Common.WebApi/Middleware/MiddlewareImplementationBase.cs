using System.Security.Claims;
using Common.Messages;
using Common.PluginFactory.Interface;
using Common.WebCommon.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
namespace Common.WebApi.Middleware;
public abstract class MiddlewareImplementationBase
{
    protected readonly IPluginFactory PluginFactory;
    protected readonly AppSettingsCommon AppSettings;
    protected readonly ILogger<MiddlewareImplementationBase> Logger;
    protected readonly IUserMessages UserMessages;
    protected readonly IWebHostEnvironment WebHostEnvironment;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    /// <param name="appSettingsApi"></param>
    protected MiddlewareImplementationBase(
        ILogger<MiddlewareImplementationBase> logger,
        IPluginFactory pluginFactory,
        AppSettingsCommon appSettings)
    {
        Logger = logger;
        PluginFactory = pluginFactory;
        AppSettings = appSettings;
        UserMessages = PluginFactory.GetType<IUserMessages>();
        WebHostEnvironment = PluginFactory.GetType<IWebHostEnvironment>();
    }



    /// <summary>
    /// Obtiene un Header
    /// </summary>
    /// <param name="headerName"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    protected static string GetHeaderValueFromName(string headerName, HttpContext context)
        => context.Request.Headers.FirstOrDefault(p => p.Key.Equals(headerName, StringComparison.InvariantCultureIgnoreCase)).Value;

    /// <summary>
    /// Obtiene un Claim
    /// </summary>
    /// <param name="headerName"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    protected static string GetClaimValueOrNullFromName(string headerName, HttpContext context)
        => context.User?.Claims?.FirstOrDefault(p => p.Type.Equals(headerName, StringComparison.InvariantCultureIgnoreCase))?.Value;


}
