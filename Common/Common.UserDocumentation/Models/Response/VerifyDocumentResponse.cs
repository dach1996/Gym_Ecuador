
using Newtonsoft.Json;

namespace Common.UserDocumentation.Models.Response;
/// <summary>
/// Respuesta para verificar documento
/// </summary>
public class VerifyDocumentResponse
{
    /// <summary>
    /// Identificación 
    /// </summary>
    /// <value></value>
    public string Identification { get; set; }

    /// <summary>
    /// Nombres y Apellidos 
    /// </summary>
    /// <value></value>
    public string FullName { get; set; }

    /// <summary>
    /// Nombres 
    /// </summary>
    /// <value></value>
    public string Names { get; set; }

    /// <summary>
    /// Apellidos 
    /// </summary>
    /// <value></value>
    public string LastNames { get; set; }

}


/// <summary>
/// Base de respuesta
/// </summary>
public class InternalVerifyDocumentResponse
{
    /// <summary>
    /// Data
    /// </summary>
    /// <value></value>
    [JsonProperty("data")]
    public Data Data { get; set; }
}

/// <summary>
/// Informaciòn
/// </summary>
public class Data
{
    /// <summary>
    /// Respuesta
    /// </summary>
    /// <value></value>
    [JsonProperty("response")]

    public Response Response { get; set; }
}

/// <summary>
/// Respuesta de verificación Interna
/// </summary>
public class Response
{
    /// <summary>
    /// Identificación 
    /// </summary>
    /// <value></value>
    [JsonProperty("identificacion")]
    public string Identification { get; set; }

    /// <summary>
    /// Nombres y Apellidos 
    /// </summary>
    /// <value></value>
    [JsonProperty("nombreCompleto")]
    public string FullName { get; set; }

    /// <summary>
    /// Nombres 
    /// </summary>
    /// <value></value>
    [JsonProperty("nombres")]
    public string Names { get; set; }

    /// <summary>
    /// Apellidos 
    /// </summary>
    /// <value></value>
    [JsonProperty("apellidos")]
    public string LastNames { get; set; }
}
