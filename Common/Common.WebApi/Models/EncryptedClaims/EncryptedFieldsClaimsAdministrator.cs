
using Common.WebCommon.Models;

namespace Common.WebApi.Models.EncryptedClaims;

public class EncryptedFieldsClaimsAdministrator : EncryptedFieldClaimCommon
{
    /// <summary>
    /// Alcances del rol en contexto
    /// </summary>
    /// <value></value>
    public List<RoleScopeClaim> RoleScopeClaims { get; set; }
}
/// <summary>
/// Alcance del rol
/// /// </summary>
public class RoleScopeClaim
{
    /// <summary>
    /// Identificador del alcance del rol
    /// </summary>
    /// <value></value>
    public int? Identifier { get; set; }

    /// <summary>
    /// Alcance del rol
    /// </summary>
    /// <value></value>
    public byte Scope { get; set; }

    /// <summary>
    /// Id del rol
    /// </summary>
    /// <value></value>
    public int RoleId { get; set; }
}