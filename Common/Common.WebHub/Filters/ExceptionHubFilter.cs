using System.Diagnostics;
using Common.Utils.Extensions;
using Common.WebHub.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Common.WebHub.Filters;
/// <summary>
/// Agregar contexto 
/// </summary>
public class ExceptionHubFilter(ILogger<ExceptionHubFilter> logger) : HubFilterBase(logger), IHubFilter
{

    /// <summary>
    /// Contexto
    /// </summary>
    /// <param name="invocationContext"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async ValueTask<object> InvokeMethodAsync(
      HubInvocationContext invocationContext,
      Func<HubInvocationContext, ValueTask<object>> next)
    {
        var requestUniqueId = Guid.NewGuid().ToString();
        var timer = Stopwatch.StartNew();
        try
        {
            invocationContext.Context.Items["RequestUniqueId"] = requestUniqueId;
            Logger.LogInformation("{@RequestUniqueId} - ConnectionId: {@ConnectionId} - Method:{@HubMethod} - Arguments: {@Body}", requestUniqueId, invocationContext.Context.ConnectionId, invocationContext.HubMethodName, invocationContext.HubMethodArguments.ToJson());
            var h = await next(invocationContext);
            return h;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "{@RequestUniqueId} - {@Message}", requestUniqueId, ex.Message);
            return new ValueTask<GenericHubResponse<string>>(GenericHubResponse<string>.Error(ex.Message));
        }
        finally
        {
            timer.Stop();
            Logger.LogInformation("Tiempo de ejecución: {@RequestUniqueId} - {@Time} ms", requestUniqueId, timer.Elapsed.TotalMilliseconds);
        }
    }
}