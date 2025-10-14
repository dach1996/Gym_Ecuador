namespace Common.Utils.Extensions;
public static class TypeExtensions
{
    /// <summary>
    /// Convierte un Enum en una lista
    /// </summary>
    public static IEnumerable<TEnum> ToListEnum<TEnum>(this Type type) where TEnum : struct 
        => Enum.GetValues(type).Cast<TEnum>();
}
