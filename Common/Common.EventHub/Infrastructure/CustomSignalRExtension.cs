using Common.EventHub.Implementation.CustomSignalRService.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace Common.EventHub.Infrastructure;
internal static class CustomSignalRExtension
{
    public static void AddCustomSignalRService(this IServiceCollection services)
    {
        services.AddHttpClient($"{EventHubImplementationName.CustomSignalR}")
            .AddHttpMessageHandler<AddAuthorizationDelegatinHandler>();
        services.AddTransient<AddAuthorizationDelegatinHandler>();

    }
}
