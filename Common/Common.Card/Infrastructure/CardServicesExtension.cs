using Autofac;
using Common.Card.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Card.Infrastructure;
public static class CardExtension
{
    public static void UseCardServices(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<RapidapiCardServicesImplementation>().Keyed<ICardServices>($"{CardImplementationName.Rapidapi.ToString().ToUpper()}");
    }

    public static void AddCardServices(this IServiceCollection services)
    {
        services.AddRapidapiCardServices();
    }
}
