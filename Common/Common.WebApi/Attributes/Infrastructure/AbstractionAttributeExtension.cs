
using Autofac;
using Common.WebApi.Attributes.ContextAttribute.Interface;
using Common.PluginFactory.Extensions;
namespace Common.WebApi.Attributes.Infrastructure;
/// <summary>
/// Configuración de abstracciones de Middleware
/// </summary>
public static class AbstractionAttributeExtension
{
    public static void UseAttributeAbstractionsAssemblies(this ContainerBuilder builder)
    {
        builder.ScanAssembliesFor<IAddContext>();
    }
}