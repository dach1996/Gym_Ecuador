using Common.Card.Implementation.Rapidapi.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Card.Infrastructure;
internal static class RapidapiExtension
{
    public static void AddRapidapiCardServices(this IServiceCollection services)
    {
        services.AddHttpClient($"{CardImplementationName.Rapidapi}").AddHttpMessageHandler<AddHeadersDelegatinHandler>();
        services.AddTransient<AddHeadersDelegatinHandler>();
    }
}
