using Common.UserDocumentation;
using Microsoft.Extensions.DependencyInjection;

namespace Common.UserDocumentation.Infrastructure;
internal static class BusPlaceExtension
{
    public static void AddBusPlaceServices(this IServiceCollection services)
    {
        services.AddHttpClient($"{DocumentationImplementationName.BusPlace}");
    }
}

