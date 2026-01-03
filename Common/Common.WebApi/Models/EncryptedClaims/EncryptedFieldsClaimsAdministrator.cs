
using Common.WebCommon.Models;

namespace Common.WebApi.Models.EncryptedClaims;

public class EncryptedFieldsClaimsAdministrator : EncryptedFieldClaimCommon
{
    /// <summary>
    /// Guid de usuario
    /// </summary>
    /// <value></value>
    public int UserId { get; set; }

    /// <summary>
    /// Fecha de creación de la sesión de Cache
    /// </summary>
    /// <value></value>
    public DateTime UserInformationCacheDateTimeCreation { get; set; }

    /// <summary>
    /// Indica si el usuario es super administrador
    /// </summary>
    /// <value></value>
    public bool IsSuperAdmin { get; set; }

    /// <summary>
    /// Id de gimnasios en contexto
    /// </summary>
    /// <value></value>
    public List<GymRoleClaim> GymRoleClaims { get; set; }
}
/// <summary>
/// Rol de gimnasio
/// /// </summary>
public class GymRoleClaim
{
    /// <summary>
    /// Id de gimnasio
    /// </summary>
    /// <value></value>
    public int GymId { get; set; }

    /// <summary>
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