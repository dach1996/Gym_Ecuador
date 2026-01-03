using Newtonsoft.Json;

namespace Common.UserDocumentation.Implementation.ServiciosEcuador.Models.Response;

/// <summary>
/// Respuesta del servicio GetByDocumentNumber
/// </summary>
public class PersonByDocumentResponse
{
    /// <summary>
    /// Código de respuesta
    /// </summary>
    [JsonProperty("code")]
    public int Code { get; set; }

    /// <summary>
    /// Tipo de respuesta
    /// </summary>
    [JsonProperty("responseType")]
    public string ResponseType { get; set; }

    /// <summary>
    /// Mensaje de respuesta
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }

    /// <summary>
    /// Contenido de la respuesta
    /// </summary>
    [JsonProperty("content")]
    public Content Content { get; set; }
}

/// <summary>
/// Contenido de la respuesta
/// </summary>
public class Content
{
    /// <summary>
    /// Indicador de mostrar mensaje
    /// </summary>
    [JsonProperty("showMessage")]
    public bool ShowMessage { get; set; }

    /// <summary>
    /// Información de la persona
    /// </summary>
    [JsonProperty("person")]
    public Person Person { get; set; }
}

/// <summary>
/// Información de la persona
/// </summary>
public class Person
{
    /// <summary>
    /// Identificador único
    /// </summary>
    [JsonProperty("guid")]
    public string Guid { get; set; }

    /// <summary>
    /// Número de documento
    /// </summary>
    [JsonProperty("documentNumber")]
    public string DocumentNumber { get; set; }

    /// <summary>
    /// Nombres
    /// </summary>
    [JsonProperty("names")]
    public string Names { get; set; }

    /// <summary>
    /// Apellidos
    /// </summary>
    [JsonProperty("lastNames")]
    public string LastNames { get; set; }

    /// <summary>
    /// Nombre completo
    /// </summary>
    [JsonProperty("fullName")]
    public string FullName { get; set; }
}

