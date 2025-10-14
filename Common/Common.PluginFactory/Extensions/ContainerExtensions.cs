using Autofac;
using Common.PluginFactory.Implementation;
using Common.PluginFactory.Interface;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using System.Runtime.Loader;
namespace Common.PluginFactory.Extensions;
public static class ContainerExtensions
{

    private static List<Assembly> _localAssemblies;

    /// <summary>
    /// Registra los Assemblies mediante un keyword
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="containerBuilder"></param>
    /// <param name="keyword"></param>
    public static void ScanAssembliesFor<T>(this ContainerBuilder containerBuilder, string keyword)
    {
        var assemblies = LoadLocalAssemblies();
        _ = containerBuilder.RegisterAssemblyTypes(assemblies.ToArray())
            .Where(t => t.Name.Contains(keyword))
            .AsImplementedInterfaces()
            .Keyed<T>(t => t.Name.Replace(keyword, "").ToUpper());
        _ = containerBuilder.RegisterType<PluginFactoryAutofac>().As<IPluginFactory>().IfNotRegistered(typeof(IPluginFactory));
    }

    /// <summary>
    /// Registra los Assemblies metidante el tipo de interfas 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="containerBuilder"></param>
    /// <param name="keyword"></param>
    public static void ScanAssembliesFor<T>(this ContainerBuilder containerBuilder)
    {
        var typeName = typeof(T).Name;
        if (typeName is not null && typeName.StartsWith('I'))
            typeName = typeName.Remove(0, 1);
        var assemblies = LoadLocalAssemblies();
        _ = containerBuilder.RegisterAssemblyTypes(assemblies.ToArray())
            .Where(t => t.Name.Contains(typeName))
            .AsImplementedInterfaces()
            .Keyed<T>(t => t.Name.Replace(typeName, "").ToUpper());
        _ = containerBuilder.RegisterType<PluginFactoryAutofac>().As<IPluginFactory>().IfNotRegistered(typeof(IPluginFactory));
    }


    /// <summary>
    /// Carga todas las librerías 
    /// </summary>
    /// <returns></returns>
    private static List<Assembly> LoadLocalAssemblies()
    {
        if (_localAssemblies != null) return _localAssemblies;
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        _localAssemblies = assemblies;
        return _localAssemblies;
    }
}