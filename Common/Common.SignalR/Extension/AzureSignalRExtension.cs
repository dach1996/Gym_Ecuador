using Common.SignalR.Model;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Common.SignalR.Extension;

public static class AzureSignalRExtension
{
    public static void AddAzureSignalR(this IServiceCollection services, IConfiguration configuration, Action<HubOptions> configure)
    {
        //Agrega signalR
        var signalRConfiguration = configuration.GetSection(nameof(SignalRConfiguration))
            .Get<SignalRConfiguration>();
        var service = services.AddSignalR(configure);
        var azureConnectionString = signalRConfiguration?.AzureConnectionString;
        if (!string.IsNullOrEmpty(azureConnectionString))
            service.AddAzureSignalR(azureConnectionString);
    }
}