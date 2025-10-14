using Common.Messages.Models;
using Common.WebApi.Models.Enum;
using Common.WebCommon.Models;

namespace Common.WebApi.Models;

/// <summary>
/// Contexto de auditoría del request
/// </summary>
public class ContextRequest : CommonContextRequest
{

    /// <summary>
    /// Headers de auditoría
    /// </summary>
    public Header Headers { get; set; }

    /// <summary>
    /// Host del request 
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    /// Path del request
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Parámetros presentes en la URL
    /// </summary>
    public string QueryParameters { get; set; }

    /// <summary>
    /// URL completa del request
    /// </summary>
    public string UrlRequest { get; set; }

    /// <summary>
    /// Fecha y hora del request
    /// </summary>
    public DateTime DateRequest { get; set; }

    /// <summary>
    /// IP de origen
    /// </summary>
    public string IpOrigin { get; set; }

    /// <summary>
    /// Claim SUB del token JWT
    /// </summary>
    public string CurrentSubClaim { get; set; }

    /// <summary>
    /// Alcance del request extraído del token JWT
    /// </summary>
    public string Scope { get; set; }

    /// <summary>
    /// Cantidad de veces que se ha realizado el refresh token
    /// </summary>
    public int CountRefresh { get; set; }

    /// <summary>
    /// Custom Claims
    /// </summary>
    public CustomClaims CustomClaims { get; set; }
}

/// <summary>
/// Custom Claims
/// </summary>
public class CustomClaims
{

    /// <summary>
    /// Id de usuario 
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Id de usuario 
    /// </summary>
    public int? PersonId { get; set; }

    /// <summary>
    /// Correo Electrónico
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Primer Nombre
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Apellido
    /// </summary>
    public string Surname { get; set; }

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

    /// <summary>
    /// Identificador del dispositivo generado por el dispositivo móvil
    /// </summary>
    public string MobileId { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    public Guid? UserGuid { get; set; }

    /// <summary>
    /// Id del dipositivo almacenando en base de datos
    /// </summary>
    public int? DeviceId { get; set; }

    /// <summary>
    /// Método para configurar usuario
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="userName"></param>
    public void ConfigureUser(int userId, string userName)
    {
        UserId = userId;
        UserName = userName;
    }

    /// <summary>
    /// Método para configurar usuario
    /// </summary>
    public void CleanUser()
    {
        UserId = null;
        UserName = string.Empty;
    }
}

/// <summary>
/// Headers
/// </summary>
public class Header
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
    /// Identificador del dispositivo
    /// </summary>
    public string DeviceId { get; set; }

    /// <summary>
    /// Versión de la aplicación
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// Modelo de dispositivo
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Marca del dispositivo
    /// </summary>
    public string Brand { get; set; }

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

    /// <summary>
    /// Sistema Operativo del Dispositivo
    /// </summary>
    /// <value></value>
    public string SystemOperation { get; set; }

    /// <summary>
    /// Verifica si tiene servicios de Google
    /// </summary>
    /// <value></value>
    public bool HasGoogleServices { get; set; }
}