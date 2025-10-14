using System.Text.RegularExpressions;
using Common.Cache.CacheExcpetion;
using Common.Cache.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
namespace Common.Cache.Implementation.DotNet;
/// <summary>
/// Constructor
/// </summary>
/// <param name="cacheConfigurationModel"></param>
/// <param name="memoryCache"></param>
/// <returns></returns>
public class DotNetAdministratorCache(
    CacheConfigurationModel cacheConfigurationModel,
    IMemoryCache memoryCache,
    ILogger<DotNetAdministratorCache> logger) : BaseCache(cacheConfigurationModel)
{
    private readonly IMemoryCache _memoryCache = memoryCache;

    /// <summary>
    /// Elimina un dato cache
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public override async Task RemoveAsync(string key)
    => await Task.Run(() => _memoryCache.Remove(key));

    /// <summary>
    ///  Configura un dato en cache
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="minutes"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public override async Task SetAsync<T>(string key, T value, int? seconds = null, bool slidingExpiration = false)
        => _ = await Task.Run(() =>
            {
                var options = new MemoryCacheEntryOptions();
                seconds ??= CacheConfigurationModel.ConfigurationByKey?.FirstOrDefault(t => t.Key == key).Value?.DurationSeconds
                              ?? CacheConfigurationModel.ConfigurationByRegularExpression.FirstOrDefault(where => Regex.IsMatch(key, where.Key, RegexOptions.None, TimeSpan.FromMilliseconds(100))).Value?.DurationSeconds
                              ?? CacheConfigurationModel.DefaultDurationSeconds;
                _ = slidingExpiration ? options.SetSlidingExpiration(TimeSpan.FromSeconds(seconds.Value)) : options.SetAbsoluteExpiration(TimeSpan.FromSeconds(seconds.Value));
                logger.LogDebug("Cache Registrado: {@Key} - Tiempo: {@Seconds} segundos - Tipo : {@Type}", key, seconds, slidingExpiration ? "SetSlidingExpiration" : "SetAbsoluteExpiration");
                return _memoryCache.Set(key, value, options);
            });

    /// <summary>
    /// Obtiene un dato cache
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public override async Task<T> TryGetAsync<T>(string key)
    => await Task.Run(() =>
    {
        _memoryCache.TryGetValue(key, out T result);
        return result;
    });

    /// <summary>
    /// Verifica si existe un Key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public override Task<bool> ExistKeyAsync(string key)
        => Task.FromResult(_memoryCache.TryGetValue(key, out var _));


    /// <summary>
    /// Obtiene un valor de cache o lo injecta si no lo encuentra
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callbackAsync"></param>
    /// <param name="minutes"></param>
    /// <param name="throwExceptionIfNotFound"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async override Task<T> TryGetOrSetAsync<T>(
        string key,
        Func<Task<T>> callbackAsync,
        int? seconds = null,
        bool slidingExpiration = false,
        bool throwExceptionIfNotFound = true)
    {
        return await _memoryCache.GetOrCreateAsync(key, async (cacheEntry) =>
        {
            seconds ??= CacheConfigurationModel.ConfigurationByKey?.FirstOrDefault(where => where.Key == key).Value?.DurationSeconds
                ?? CacheConfigurationModel.ConfigurationByRegularExpression.FirstOrDefault(where => Regex.IsMatch(key, where.Key, RegexOptions.None, TimeSpan.FromMilliseconds(100))).Value?.DurationSeconds
                ?? CacheConfigurationModel.DefaultDurationSeconds;
            _ = slidingExpiration ? cacheEntry.SetSlidingExpiration(TimeSpan.FromSeconds(seconds.Value)) : cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(seconds.Value));
            logger.LogDebug("Cache Registrado: {@Key} - Tiempo: {@Seconds} segundos - Tipo : {@Type}", key, seconds, slidingExpiration ? "SetSlidingExpiration" : "SetAbsoluteExpiration");
            var result = await callbackAsync().ConfigureAwait(false);
            if (result is null && throwExceptionIfNotFound)
                throw new CustomCacheException($"No de pudo encontrar en cache ni asignar valores a la Key: '{key}'");
            return result;
        }).ConfigureAwait(false);
    }
}
