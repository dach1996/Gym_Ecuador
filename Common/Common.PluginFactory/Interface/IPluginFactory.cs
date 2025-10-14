namespace Common.PluginFactory.Interface;

public interface IPluginFactory
{
    /// <summary>
    /// Obtiene la implementación en base a un Key
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    T GetPlugin<T>(string key, bool throwExceptionWhenNotFound = true);

    /// <summary>
    /// Obtiene las implementaciones en base a unas Keys
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="keys"></param>
    /// <returns></returns>
    IDictionary<string, T> GetPlugins<T>(IEnumerable<string> keys, bool throwExceptionWhenNotFound = true);

    /// <summary>
    /// Obtiene la implementación de una interfas 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T GetType<T>(bool throwExceptionWhenNotFound = true);
}
