namespace LogicApi.Model.Response.Security;
/// <summary>
/// Modelo de respuesta para obtener Llave pública
/// </summary>
public class DecryptResponse
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dictionaryTextsDecrypt"></param>
    public DecryptResponse(IDictionary<string, string> dictionaryTextsDecrypt) => DictionaryTextsDecrypt = dictionaryTextsDecrypt;

    /// <summary>
    /// Diccionario de Textos desencriptados
    /// </summary>
    /// <value></value>
    public IDictionary<string, string> DictionaryTextsDecrypt { get; set; }

}
