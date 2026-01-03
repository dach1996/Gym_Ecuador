using Common.UserDocumentation;
using Common.UserDocumentation.Implementation.ServiciosEcuador.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace Common.UserDocumentation.Infrastructure;

internal static class ServiciosEcuadorExtension
{
    public static void AddServiciosEcuadorServices(this IServiceCollection services)
    {
        services.AddHttpClient($"{DocumentationImplementationName.ServiciosEcuador}").AddHttpMessageHandler<AddAuthorizationDelegatingHandler>();
        services.AddTransient<AddAuthorizationDelegatingHandler>();
    }
}

