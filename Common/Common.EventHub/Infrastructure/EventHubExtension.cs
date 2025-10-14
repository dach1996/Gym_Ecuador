using Autofac;
using Common.EventHub.Implementation.CustomSignalRService;
using Microsoft.Extensions.DependencyInjection;

namespace Common.EventHub.Infrastructure;
public static class EventHubExtension
{
    public static void UseEventHub(this ContainerBuilder containerBuilder) 
        => containerBuilder.RegisterType<CustomSignalREventHub>().Keyed<IEventHub>($"{EventHubImplementationName.CustomSignalR.ToString().ToUpper()}");

    public static void AddEventHub(this IServiceCollection services) 
        => services.AddCustomSignalRService();
}
