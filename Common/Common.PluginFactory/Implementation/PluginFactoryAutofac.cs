using Autofac;
using Common.PluginFactory.Interface;
using Microsoft.Extensions.Logging;

namespace Common.PluginFactory.Implementation;
public class PluginFactoryAutofac : IPluginFactory
{
    private readonly ILifetimeScope _lifetimeScope;

    private readonly ILogger<PluginFactoryAutofac> _logger;

    public PluginFactoryAutofac(ILogger<PluginFactoryAutofac> logger, ILifetimeScope lifetimeScope)
    {
        _lifetimeScope = lifetimeScope;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene la implementación en base a un Key
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public T GetPlugin<T>(string key, bool throwExceptionWhenNotFound = true)
    {
        if (_lifetimeScope.TryResolveKeyed(key.ToUpper(), typeof(T), out var implementation))
            return (T)implementation;
        if (throwExceptionWhenNotFound)
            throw new ArgumentNullException(key, $"No se encuentra una implementación para la interfaz {typeof(T)} con la key: {key}");
        return default;
    }

    /// <summary>
    /// Obtiene las implementaciones en base a Keys
    /// </summary>
    /// <param name="keys"></param>
    /// <param name="throwExceptionWhenNotFound"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public IDictionary<string, T> GetPlugins<T>(IEnumerable<string> keys, bool throwExceptionWhenNotFound = true)
    {
        var implementations = new Dictionary<string, T>();
        foreach (var key in keys)
            implementations.Add(key, GetPlugin<T>(key, throwExceptionWhenNotFound));
        return implementations;
    }

    /// <summary>
    /// Obtiene la implementación de una interfas 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetType<T>(bool throwExceptionWhenNotFound = true)
    {
        if (_lifetimeScope.TryResolve(typeof(T), out var implementation))
            return (T)implementation;
        if (throwExceptionWhenNotFound)
            throw new InvalidOperationException($"No se encuentra una implementación para la interfaz {typeof(T)}");
        _logger.LogWarning("No se encuentra una implementación para el tipo {@Type}.", typeof(T));
        return default;
    }
}
