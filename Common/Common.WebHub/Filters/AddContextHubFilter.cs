using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Common.WebHub.Filters;
/// <summary>
/// Agregar contexto 
/// </summary>
public class AddContextHubFilter(ILogger<AddContextHubFilter> logger) : HubFilterBase(logger), IHubFilter
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
        return await next(invocationContext);
    }
}