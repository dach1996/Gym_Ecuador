using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Common.PluginFactory.Interface;

namespace Common.WebApi.Middleware;

public class TimeDurationRequestMiddleware : MiddlewareBase
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    /// <param name="pluginFactory"></param>
    public TimeDurationRequestMiddleware(
        RequestDelegate next,
        ILogger<TimeDurationRequestMiddleware> logger,
        IPluginFactory pluginFactory)
        : base(
            next,
            logger,
            pluginFactory)
    {
    }

    /// <summary>
    /// Invoke
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        var stopwatch = new Stopwatch();
        try
        {
            stopwatch.Restart();
            await Next(httpContext).ConfigureAwait(false);
        }
        finally
        {
            stopwatch.Stop();
            Logger.LogInformation("Request: {@TraceIdentifier} - Tiempo de Respuesta: {@ResponseTime}ms", httpContext.TraceIdentifier, stopwatch.ElapsedMilliseconds);
        }
    }
}
