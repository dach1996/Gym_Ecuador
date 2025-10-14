
using System.Collections.Concurrent;
namespace Common.Utils.Extensions;
public static class ConcurrentDictionaryExtension
{
    /// <summary>
    /// Retorna el primer valor o Null
    /// </summary>
    /// <param name="inputDictionary"></param>
    /// <param name="keySearch"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TU"></typeparam>
    /// <returns></returns>
    public static TU? TryGetValueOrDefault<T, TU>(this ConcurrentDictionary<T, TU> inputDictionary, T keySearch) where TU : struct
        => inputDictionary.TryGetValue(keySearch, out var result) ? result : null;

    /// <summary>
    /// Retorna el primer valor o Null
    /// </summary>
    /// <param name="inputDictionary"></param>
    /// <param name="keySearch"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TU"></typeparam>
    /// <returns></returns>
    public static TU TryGetOrDefaultValue<T, TU>(this ConcurrentDictionary<T, TU> inputDictionary, T keySearch) where TU : class
        => inputDictionary.TryGetValue(keySearch, out var result) ? result : null;
}
