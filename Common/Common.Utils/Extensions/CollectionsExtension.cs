namespace Common.Utils.Extensions;

public static class CollectionsExtension
{
    /// <summary>
    /// Permite juntar una lista como string con un separador
    /// </summary>
    /// <param name="list"></param>
    /// <param name="charInput"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string Join<T>(this IEnumerable<T> list, char charInput)
    => string.Join(charInput, list);

    /// <summary>
    /// Permite juntar una lista como string con un separador
    /// </summary>
    /// <param name="list"></param>
    /// <param name="stringInput"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string Join<T>(this IEnumerable<T> list, string stringInput = ",")
    => string.Join(stringInput, list);

    /// <summary>
    /// Encuentra los registros duplicados en base a un Key
    /// </summary>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TSource> Duplicates<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
    {
        var grouped = source.GroupBy(selector);
        var moreThan1 = grouped.Where(i => i.Count() > 1);
        return moreThan1.SelectMany(i => i);
    }

    /// <summary>
    /// Encuentra registros duplicados 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TSource> Duplicates<TSource>(this IEnumerable<TSource> source)
    => source.Duplicates(i => i);

    /// <summary>
    /// Encuentra los registros únicos en base a un key
    /// </summary>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TSource> Unique<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
    {
        var grouped = source.GroupBy(selector);
        var moreThan1 = grouped.Where(i => i.Count() == 1);
        return moreThan1.SelectMany(i => i);
    }

    /// <summary>
    /// Encuentra registors únicos
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TSource> Unique<TSource>(this IEnumerable<TSource> source)
    => source.Duplicates(i => i);

    /// <summary>
    ///  Elimina varios elementos de una lista
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public static List<TSource> Remove<TSource>(this List<TSource> source, List<TSource> sourceToRemove)
    {
        sourceToRemove.ForEach(register => source.Remove(register));
        return source;
    }

    /// <summary>
    /// Encuentra los elementos únicos en base a una key.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TKey> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
    => source.GroupBy(selector).Select(g => g.Key);

    /// <summary>
    /// Convierte una lista de String en una lista de Enumerables
    /// </summary>
    public static IEnumerable<T> ToListEnum<T>(this IEnumerable<string> source) where T : struct
    => source?.Select(x => (T)Enum.Parse(typeof(T), x, true));


    /// <summary>
    /// Transforma lista de enteros en null
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static IEnumerable<int?> ToNulleable(this IEnumerable<int> list)
    => list.Select(t => (int?)t);

    /// <summary>
    /// Verifica si la lista es null o está vacía
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
    => list is null || (!list.Any());

    /// <summary>
    /// Elimina de una lista en base a otra lista
    /// </summary>
    /// <param name="source"></param>
    /// <param name="listToRemove"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TSource> RemoveByList<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> listToRemove)
    {
        var list = source.ToList();
        foreach (var item in listToRemove)
            list.Remove(item);
        return list;
    }

    /// <summary>
    /// Elimina de una lista en base a una condición
    /// </summary>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="result"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TSource> RemoveWhere<TSource>(
        this IEnumerable<TSource> source,
        Func<TSource, bool> predicate,
         out IEnumerable<TSource> result)
    {
        var list = source.ToList();
        var listToRemove = list.Where(predicate).ToArray();
        foreach (var item in listToRemove)
            list.Remove(item);
        result = listToRemove;
        return list;
    }


    /// <summary>
    /// Encuentra los elementos que no hacen Match entre 2 listas (Full Outer Join)
    /// </summary>
    /// <param name="source"></param>
    /// <param name="listOuterJoin"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TSource> FullOuterJoin<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> listOuterJoin)
        => source.Except(listOuterJoin).Concat(listOuterJoin.Except(source));

    /// <summary>
    /// Verifica si todos los elementos de una lista "list" existen en "elementsContains"
    /// </summary>
    /// <param name="list">Lista de entrada a Verificar</param>
    /// <param name="elementsContains">Lista contra la cuál comprar</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>Lista de Elementos que no coincidieron</returns>
    public static IEnumerable<T> ContainsIn<T>(this IEnumerable<T> list, IEnumerable<T> elementsContains)
    {
        if (list.IsNullOrEmpty())
            return list;
        return list.Where(t => !elementsContains.Contains(t));
    }

    /// <summary>
    /// Verifica si todos los elementos de una lista "list" existen en "elementsContains"
    /// </summary>
    /// <param name="list">Lista de entrada a Verificar</param>
    /// <param name="elementsContains">Lista contra la cuál comprar</param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns>Lista de Elementos que no coincidieron</returns>
    public static bool ContainsAll<TSource>(this IEnumerable<TSource> list, IEnumerable<TSource> elementsContains, out IEnumerable<TSource> result)
    {
        result = elementsContains;
        if (list.IsNullOrEmpty())
            return false;
        result = list.Where(t => !elementsContains.Contains(t));
        return result.IsNullOrEmpty();
    }

    /// <summary>
    /// Verifica si existe algún elemento que coincida y los obtiene
    /// </summary>
    /// <param name="list"></param>
    /// <param name="predicate"></param>
    /// <param name="result"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public static bool Any<TSource>(this IEnumerable<TSource> list, Func<TSource, bool> predicate, out IEnumerable<TSource> result)
    {
        result = [];
        var any = list.Any(predicate);
        if (list.Any(predicate))
            result = list.Where(predicate);
        return any;
    }

    /// <summary>
    /// Verifica si existe elementos repetidos y retorna la lista de elementos
    /// </summary>
    /// <param name="list">Lista de Elementos</param>
    /// <param name="predicate">Comparación </param>
    /// <param name="result">Lista con valores repetidos</param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public static bool ExistDuplicates<TSource, TKey>(this IEnumerable<TSource> list, Func<TSource, TKey> selector, out IEnumerable<TKey> result)
    {
        result = list.Duplicates(selector).Select(selector);
        return !result.IsNullOrEmpty();
    }

    /// <summary>
    /// Permite obtener el primer elemento de una lista o lanzar una excepción
    /// </summary>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="exception"></param>
    /// <typeparam name="T">Tipo de dato</typeparam>
    /// <returns>Primer elemento de la lista o excepción</returns>
    public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate, Exception exception)
        => source.FirstOrDefault(predicate) ?? throw exception;

}
