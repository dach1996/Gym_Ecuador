using System.Reflection;
using Common.SignalR.CustomAttribute;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
namespace Common.SignalR.Extension;

/// <summary>
/// Clase Estática para Run
/// </summary>
public static class WebApplicationExtension
{
    public static HubEndpointConventionBuilder MapHub<THub>(this IEndpointRouteBuilder applicationBuilder) where THub : Hub
    {
        var path = ((HubClassNameAttribute[])typeof(THub).GetCustomAttributes(typeof(HubClassNameAttribute), false))
            ?.FirstOrDefault()?.PathName
            ?? typeof(THub).Name.Replace(nameof(Hub), "");
        // Registra cada hub automáticamente
        return applicationBuilder.MapHub<THub>($"/{path}");
    }
}