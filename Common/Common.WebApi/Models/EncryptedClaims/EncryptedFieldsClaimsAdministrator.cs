
using Common.WebCommon.Models;

namespace Common.WebApi.Models.EncryptedClaims;

public class EncryptedFieldsClaimsAdministrator : EncryptedFieldClaimCommon
{
    /// <summary>
    /// Id de usuario 
    /// </summary>
    public Guid UserGuid { get; set; }

    /// <summary>
    /// Fecha de creación de la sesión de Cache
    /// </summary>
    /// <value></value>
    public DateTime UserInformationCacheDateTimeCreation { get; set; }

    /// <summary>
    /// Id de establecimiento en contexto
    /// </summary>
    /// <value></value>
    public int EstablishmentId { get; set; }

    /// <summary>
    /// Id de sucursales permitidos
    /// </summary>
    /// <value></value>
    public int EstablishmentBranchIds { get; set; }

    /// <summary>
    /// Id de sucursales permitidos
    /// </summary>
    /// <value></value>
    public Guid EstablishmentBranchGuids { get; set; }

    /// <summary>
    /// Id de Veterinario
    /// </summary>
    /// <value></value>
    public int VeterinarianId { get; set; }

    /// <summary>
    /// Guid de sucursal de establecimiento
    /// </summary>
    /// <value></value>
    public Guid EstablishmentBranchGuid { get; set; }

    
}