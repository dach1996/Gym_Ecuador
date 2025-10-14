using Microsoft.Extensions.DependencyInjection;

namespace Common.WebHub.Filters.Intrastructure;

/// <summary>
/// Extensión para Filtros
/// </summary>
public static class HubFilterExtension
{
    public static void AddHubFilter(this IServiceCollection services)
    {
        _ = services.AddSingleton<AddContextHubFilter>();
        _ = services.AddSingleton<ExceptionHubFilter>();
        _ = services.AddSingleton<ConnectHubFilter>();
        _ = services.AddSingleton<ValidatorModelHubFilter>();
    }
}