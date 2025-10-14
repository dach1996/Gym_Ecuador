using Microsoft.Extensions.Configuration;
using Common.SignalR.Extension;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR;
using Common.WebHub.Provider;

namespace Common.WebHub.Filters.Intrastructure;

public static class SignalRExtension
{
    public static void AddCustomAzureSignalR(this IServiceCollection services, IConfiguration configuration)
    {
        //Agrega signalR
        services.AddAzureSignalR(configuration, options =>
        {
            options.AddFilter<ExceptionHubFilter>();
            options.AddFilter<ConnectHubFilter>();
            options.AddFilter<ValidatorModelHubFilter>();
            options.AddFilter<AddContextHubFilter>();
            options.EnableDetailedErrors = true;
        });
        services.AddHubFilter();
        services.AddSingleton<IUserIdProvider, GuidUserIdentifierProvider>();
    }
}