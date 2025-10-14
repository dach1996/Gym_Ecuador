using Common.Cache.Interface;
using Common.Cache.Model;

namespace Common.Cache.Implementation;
/// <summary>
/// Constructor
/// </summary>
/// <param name="cacheConfigurationModel"></param>
public abstract class BaseCache(CacheConfigurationModel cacheConfigurationModel) : IAdministratorCache
{
    /// <summary>
    /// Configuración
    /// </summary>
    protected readonly CacheConfigurationModel CacheConfigurationModel = cacheConfigurationModel;

    /// <summary>
    /// Elimina un dato del cache
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public abstract Task RemoveAsync(string key);

    /// <summary>
    /// Configura un valor en el Cache
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="seconds"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public abstract Task SetAsync<T>(string key, T value, int? seconds = null, bool slidingExpiration = false);

    /// <summary>
    /// Verifica si existe un key registrado
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public abstract Task<bool> ExistKeyAsync(string key);

    /// <summary>
    /// Obtiene un valor del Cache
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public abstract Task<T> TryGetAsync<T>(string key);

    /// <summary>
    /// Obtiene un valor o lo asigna mediante callback
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callbackAsync"></param>
    /// <param name="seconds"></param>
    /// <param name="throwExceptionIfNotFound"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public abstract Task<T> TryGetOrSetAsync<T>(string key, Func<Task<T>> callbackAsync, int? seconds = null, bool slidingExpiration = false, bool throwExceptionIfNotFound = true);

}
