using Common.UserDocumentation.Implementation.WebServicesEc.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace Common.UserDocumentation.Infrastructure;
internal static class WebServicesEcExtension
{
    public static void AddWebServicesEcServices(this IServiceCollection services)
    {
        services.AddHttpClient($"{DocumentationImplementationName.WebServicesEc}").AddHttpMessageHandler<AddAuthorizationDelegatinHandler>();
        services.AddTransient<AddAuthorizationDelegatinHandler>();

    }
}
