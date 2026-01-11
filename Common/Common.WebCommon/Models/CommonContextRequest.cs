
using System.Text.Json.Serialization;
using Common.WebCommon.Attributes;
using Common.WebCommon.Models.Enum;

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
    /// Headers de auditoría
    /// </summary>
    public HeaderCommon Headers { get; set; }

    /// <summary>
    /// Custom Claims
    /// </summary>
    public CommonCustomClaims CustomClaims { get; set; }

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


/// <summary>
/// Headers
/// </summary>
public class HeaderCommon
{
    /// <summary>
    /// Canal declarado en el request
    /// </summary>
    public Channel Channel { get; set; }

    /// <summary>
    /// Plataforma declarada en el request
    /// </summary>
    public Platform Platform { get; set; }

    /// <summary>
    /// Versión de la aplicación
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// Lenguaje
    /// </summary>
    public UserLanguage UserLanguage { get; set; }

    /// <summary>
    /// Fecha del cliente
    /// </summary>
    public DateTime? ClientDate { get; set; }

    /// <summary>
    /// Zona Horaria
    /// </summary>
    public string TimeZone { get; set; }

    /// <summary>
    /// Contenido para validación
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Secreto
    /// </summary>
    public string Secret { get; set; }

    /// <summary>
    /// TimeSpan
    /// </summary>
    public string Time { get; set; }

    /// <summary>
    /// Token de Autorización
    /// </summary>
    /// <value></value>
    public string Authorization { get; set; }
}


/// <summary>
/// Custom Claims
/// </summary>
public class CommonCustomClaims
{

 
    /// <summary>
    /// Primer Nombre
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Apellido
    /// </summary>
    public string Surname { get; set; }

    /// <summary>
    /// Nombre Completo
    /// </summary>
    public string FullName => $"{FirstName} {Surname}";

    /// <summary>
    /// Nombre de Usuario
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Jti
    /// </summary>
    public string Jti { get; set; }

    /// <summary>
    /// Sub
    /// </summary>
    public string Sub { get; set; }

    /// <summary>
    /// Scope
    /// </summary>
    public string Scope { get; set; }

    /// <summary>
    /// Refresh
    /// </summary>
    public string Refresh { get; set; }
}