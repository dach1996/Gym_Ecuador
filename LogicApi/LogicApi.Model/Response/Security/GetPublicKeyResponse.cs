namespace LogicApi.Model.Response.Security;
/// <summary>
/// Modelo de respuesta para obtener Llave pública
/// </summary>
public class GetPublicKeyResponse
{
    /// <summary>
    /// Llave publica
    /// </summary>
    public string PublicKey { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="publicKey"></param>
    public GetPublicKeyResponse(string publicKey) => PublicKey = publicKey;
}
