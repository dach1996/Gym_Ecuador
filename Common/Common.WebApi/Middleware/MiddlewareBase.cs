using Common.Messages;
using Common.PluginFactory.Interface;
using Common.WebApi.Models;
using Common.WebCommon.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
namespace Common.WebApi.Middleware;
public abstract class MiddlewareBase
{
    protected readonly IPluginFactory PluginFactory;
    protected readonly ILogger<MiddlewareBase> Logger;
    protected readonly RequestDelegate Next;
    protected readonly IUserMessages UserMessages;
    protected readonly IWebHostEnvironment WebHostEnvironment;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    /// <param name="appSettingsApi"></param>
    protected MiddlewareBase(
        RequestDelegate next,
        ILogger<MiddlewareBase> logger,
        IPluginFactory pluginFactory)
    {
        Next = next;
        Logger = logger;
        PluginFactory = pluginFactory;
        UserMessages = PluginFactory.GetType<IUserMessages>();
        WebHostEnvironment = PluginFactory.GetType<IWebHostEnvironment>();
    }

    /// <summary>
    /// Obtiene un Header
    /// </summary>
    /// <param name="headerName"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    protected static string GetHeaderValueByName(string headerName, HttpContext context)
        => context.Request.Headers.FirstOrDefault(p => p.Key.Equals(headerName, StringComparison.InvariantCultureIgnoreCase)).Value;

    /// <summary>
    /// Obtiene un Claim
    /// </summary>
    /// <param name="headerName"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    protected static Claim GetClaimByName(string headerName, HttpContext context)
        => context.User?.Claims?.FirstOrDefault(p => p.Type.Equals(headerName, StringComparison.InvariantCultureIgnoreCase));
}
