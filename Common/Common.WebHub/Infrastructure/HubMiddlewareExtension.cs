using Common.SignalR.CustomAttribute;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Common.SignalR.Extension;

namespace Common.WebHub.Infrastructure;
/// <summary>
/// Middleware Extension
/// </summary>
public static class HubMiddlewareExtension
{
    public static HubEndpointConventionBuilder MapHubMiddleware<THub, TMiddleware>(this WebApplication applicationBuilder, string path = null) where THub : Hub
    {
        path ??= ((HubClassNameAttribute[])typeof(THub).GetCustomAttributes(typeof(HubClassNameAttribute), false))
           ?.FirstOrDefault()?.PathName
           ?? typeof(THub).Name.Replace(nameof(Hub), "");
        var applicationBuilderResponse = applicationBuilder.MapHub<THub>(path);

        applicationBuilder.UseWhen(
         httpContext => httpContext.Request.Path.StartsWithSegments($"/{path}"),
         branch => branch.UseMiddleware<TMiddleware>()
        );
        return applicationBuilderResponse;
    }
}