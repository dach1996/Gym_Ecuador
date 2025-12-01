using Autofac;
using Common.PluginFactory.Extensions;
using Common.WebApi.Middleware.ConfigureContext.Interface;
using Common.WebApi.Middleware.ValidateIntegrity.Interface;
namespace Common.WebApi.Middleware.Infrastructure;
/// <summary>
/// Configuración de abstracciones de Middleware
/// </summary>
public static class AbstractionMiddlewareExtension
{
    public static void UseMiddlewareAbstractionsAssemblies(this ContainerBuilder builder)
    {
        builder.ScanAssembliesFor<IConfigureContext>();
        builder.ScanAssembliesFor<IValidateIntegrity>();
    }
}