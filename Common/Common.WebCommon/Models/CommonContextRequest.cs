
using System.Text.Json.Serialization;
using Common.WebCommon.Attributes;

namespace Common.WebCommon.Models;
/// <summary>
/// Contexto de auditoría del request
/// </summary>
public class CommonContextRequest
{

    /// <summary>
    /// Identificador de la petición enviado
    /// </summary>
    public string RequestId { get; set; }

    /// <summary>
    /// Configuración de conexión a base de datos
    /// </summary>
    /// <value></value>
    [JsonIgnore]
    [IgnoreSensible]
    public CustomConnectionStrings DataBaseConfiguration { get; set; }

    /// <summary>
    /// Timeout
    /// </summary>
    /// <value></value>
    [JsonIgnore]
    [IgnoreSensible]
    public string TimeZone { get; set; }

}