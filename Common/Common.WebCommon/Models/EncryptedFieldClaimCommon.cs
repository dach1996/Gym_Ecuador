namespace Common.WebCommon.Models;
/// <summary>
/// Datos encritados en claim
/// </summary>
public class EncryptedFieldClaimCommon
{
    /// <summary>
    /// Nombre de Usuario
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    public Guid UserGuid { get; set; }

    /// <summary>
    /// Id de usuario
    /// </summary>
    public int UserId { get; set; }
}
