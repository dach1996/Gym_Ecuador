
namespace Common.Utils.Extensions;
public static class DictionaryExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="enumerable"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this Dictionary<TKey, TValue> currentDictionary, Dictionary<TKey, TValue> newDictionary)
    {
        foreach (var item in newDictionary)
            currentDictionary.Add(item.Key, item.Value);
        return currentDictionary;
    }

    /// <summary>
    /// Retorna el primer registro encontrado o null
    /// </summary>
    /// <param name="inputDictionary"></param>
    /// <param name="predicate"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TU"></typeparam>
    /// <returns></returns>
    public static KeyValuePair<T, TU>? FirstOrNull<T, TU>(this IDictionary<T, TU> inputDictionary, Func<KeyValuePair<T, TU>, bool> predicate)
    => inputDictionary.FirstOrDefault(predicate).Equals(default(KeyValuePair<T, TU>)) ? null : inputDictionary.SingleOrDefault(predicate);

    /// <summary>
    /// Retorna el primer valor o Null
    /// </summary>
    /// <param name="inputDictionary"></param>
    /// <param name="predicate"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TU"></typeparam>
    /// <returns></returns>
    public static TU FirstValueOrDefault<T, TU>(this IDictionary<T, TU> inputDictionary, Func<KeyValuePair<T, TU>, bool> predicate)
    => inputDictionary.FirstOrDefault(predicate).Equals(default(KeyValuePair<T, TU>)) ? default : inputDictionary.SingleOrDefault(predicate).Value;

    /// <summary>
    /// Retorna el primer valor o Null comparado con el Key
    /// </summary>
    /// <param name="inputDictionary"></param>
    /// <param name="predicate"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TU"></typeparam>
    /// <returns></returns>
    public static TU FirstValueOrDefault<T, TU>(this IDictionary<T, TU> inputDictionary, T compareValue)
    => inputDictionary.FirstOrDefault(first => first.Key.Equals(compareValue)).Equals(default(KeyValuePair<T, TU>)) ? default : inputDictionary.SingleOrDefault(first => first.Key.Equals(compareValue)).Value;

    /// <summary>
    /// Retorna el primer registro encontrado o null
    /// </summary>
    /// <param name="inputDictionary"></param>
    /// <param name="predicate"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TU"></typeparam>
    /// <returns></returns>
    public static TU FirstOrDefaultValue<T, TU>(this IDictionary<T, TU> inputDictionary, Func<KeyValuePair<T, TU>, bool> predicate)
        => inputDictionary.FirstOrDefault(predicate).Equals(default(KeyValuePair<T, TU>)) ? default : inputDictionary.SingleOrDefault(predicate).Value;


    /// <summary>
    /// Obtiene el valor de la clave o lanza una excepción
    /// </summary>
    /// <param name="inputDictionary"> Diccionario </param>
    /// <param name="key"> Clave </param>
    /// <param name="exception"> Excepción </param>
    /// <returns> Valor de la clave o excepción </returns>
    /// <exception cref="Exception"> Excepción </exception>
    public static TU GetValueOrException<T, TU>(this IDictionary<T, TU> inputDictionary, T key, Exception exception)
     => inputDictionary.TryGetValue(key, out var value) ? value : throw exception;
}
