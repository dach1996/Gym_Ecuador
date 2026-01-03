using Autofac;
using Common.UserDocumentation.Implementation.BusPlace;
using Common.UserDocumentation.Implementation.ServiciosEcuador;
using Common.UserDocumentation.Implementation.WebServicesEc;
using Microsoft.Extensions.DependencyInjection;

namespace Common.UserDocumentation.Infrastructure;
public static class DocumentationServiceExtension
{
    public static void UseDocumentationServices(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<WebServicesEcDocumentationServicesImplementation>().Keyed<IDocumentationServices>($"{DocumentationImplementationName.WebServicesEc.ToString().ToUpper()}");
        containerBuilder.RegisterType<BusPlaceDocumentationServicesImplementation>().Keyed<IDocumentationServices>($"{DocumentationImplementationName.BusPlace.ToString().ToUpper()}");
        containerBuilder.RegisterType<ServiciosEcuadorDocumentationServicesImplementation>().Keyed<IDocumentationServices>($"{DocumentationImplementationName.ServiciosEcuador.ToString().ToUpper()}");
    }

    public static void AddDocumentationServices(this IServiceCollection services)
    {
        services.AddWebServicesEcServices();
        services.AddBusPlaceServices();
        services.AddServiciosEcuadorServices();
    }
}
