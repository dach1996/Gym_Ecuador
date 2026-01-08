
using Common.WebCommon.Models;
using PersistenceDb.Models.Enums;

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
    /// Identificador del rol
    /// </summary>
    /// <value></value>
    public int? Identifier { get; set; }
    
    /// <summary>
    /// Tipo de rol
    /// </summary>
    /// <value></value>
    public RoleType RoleType { get; set; }
}