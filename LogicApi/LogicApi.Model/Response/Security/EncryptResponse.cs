namespace LogicApi.Model.Response.Security;
/// <summary>
/// Modelo de respuesta para encriptar datos
/// </summary>
public class EncryptResponse
{
    /// <summary>
    /// Textos encriptados
    /// </summary>
    /// <value></value>
    public IDictionary<string, string> DictionaryEncrypted { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dictionaryEncrypted"></param>
    public EncryptResponse(IDictionary<string, string> dictionaryEncrypted) => DictionaryEncrypted = dictionaryEncrypted;
}
