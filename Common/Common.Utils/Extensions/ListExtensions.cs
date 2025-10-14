namespace Common.Utils.Extensions;
public static class ListExtensions
{
    /// <summary>
    /// Verifica si la lista es null o está vacía
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool AddIf<T>(this List<T> list, bool conditional, T item)
    {
        if (conditional)
            list.Add(item);
        return conditional;
    }
}
