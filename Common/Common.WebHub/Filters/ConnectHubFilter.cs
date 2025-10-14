using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Common.WebHub.Filters;
/// <summary>
/// Agregar contexto 
/// </summary>
public class ConnectHubFilter(ILogger<ConnectHubFilter> logger) : HubFilterBase(logger), IHubFilter
{
    /// <summary>
    ///   Conecta el hub
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
    {
        await next(context);
    }

    /// <summa ry>
    /// Desconecta el hub
    /// </summary>
    /// <param name="context"></param>
    /// <param name="exception"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnDisconnectedAsync(HubLifetimeContext context, Exception exception, Func<HubLifetimeContext, Exception, Task> next)
    {
        await next(context, exception);
    }

}