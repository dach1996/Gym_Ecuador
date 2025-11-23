using Common.WebCommon.Models;

namespace Common.WebApi.Models.ContextRequestModel;

/// <summary>
/// Contexto de auditoría del request
/// </summary>
public class ContextRequest : CommonContextRequest
{

    /// <summary>
    /// Headers de auditoría
    /// </summary>
    public new HeaderApi Headers { get; set; }

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
    public new CustomClaimsApi CustomClaims { get; set; }
}

/// <summary>
/// Custom Claims
/// </summary>
public class CustomClaimsApi : CommonCustomClaims
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
public class HeaderApi : HeaderCommon
{
    /// <summary>
    /// Identificador del dispositivo
    /// </summary>
    public string DeviceId { get; set; }

    /// <summary>
    /// Modelo de dispositivo
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Marca del dispositivo
    /// </summary>
    public string Brand { get; set; }

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