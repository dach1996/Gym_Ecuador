using Newtonsoft.Json;
namespace Common.Utils.Extensions;
/// <summary>
/// Genérico
/// </summary>
public static class GenericExtension
{
    /// <summary>
    /// Clonar un objeto
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static T Clone<T>(this T inputClone) where T : class
        => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(inputClone));

    /// <summary>
    /// Convierte un elemento y lo añade a una lista
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static IEnumerable<T> ToListElements<T>(this T inpuType) where T : class
        => new T[] { inpuType };


    /// <summary>
    /// Transforma objeto a Json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static string ToJson<T>(this T input)
        => JsonConvert.SerializeObject(input);
}