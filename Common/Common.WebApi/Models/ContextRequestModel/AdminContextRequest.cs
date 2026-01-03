using Common.WebCommon.Models;

namespace Common.WebApi.Models.ContextRequestModel;

/// <summary>
/// Contexto de auditoría del request
/// </summary>
public class AdminContextRequest : CommonContextRequest
{

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
    public new CustomClaimsAdministrator CustomClaims { get; set; }
}

/// <summary>
/// Custom Clamis
/// </summary>
public class CustomClaimsAdministrator : CommonCustomClaims
{
    /// <summary>
    /// Id de Usuario
    /// </summary>
    /// <value></value>
    public Guid? UserGuid { get; set; }

    /// <summary>
    /// Indica si el usuario es super administrador
    /// </summary>
    /// <value></value>
    public bool IsSuperAdmin { get; set; }

    /// <summary>
    /// Roles en contexto
    /// </summary>
    /// <value></value>
    public List<GymRoleContextClaim> GymRoleContextClaims { get; set; }
}


/// <summary>
/// Rol de gimnasio
/// /// </summary>
public class GymRoleContextClaim
{
    /// <summary>
    /// Id de gimnasio
    /// </summary>
    /// <value></value>
    public int GymId { get; set; }

    /// Guid de gimnasio
    /// </summary>
    /// <value></value>
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Roles en contexto
    /// </summary>
    /// <value></value>
    public List<string> Roles { get; set; }
}