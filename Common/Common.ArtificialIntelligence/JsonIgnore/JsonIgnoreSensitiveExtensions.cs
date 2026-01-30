using Newtonsoft.Json;

namespace Common.ArtificialIntelligence.JsonIgnore;

internal static class JsonIgnoreSensitiveExtensions
{

    /// <summary>
    /// Convierte un objeto en Json ignorando los datos sensibles
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="model"></param>
    /// <returns></returns>
    public static string ToJsonSensitve<T>(this T model) where T : class => JsonConvert.SerializeObject(model, JsonIgnoreSensitiveSerializerSettings);

    /// <summary>
    /// Configuración de sensibilidad
    /// </summary>
    private static JsonSerializerSettings JsonIgnoreSensitiveSerializerSettings => new()
    {
        ContractResolver = new IgnoreSensitveDefaultContractResolver(),
        Formatting = Formatting.Indented
    };

}