namespace Common.WebCommon.Models;
/// <summary>
/// Datos encritados en claim
/// </summary>
public class EncryptedFieldClaim
{
    /// <summary>
    /// Id de usuario 
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Id de persona
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// Correo Electrónico
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Nombre de Usuario
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Identificador del dispositivo generado por el dispositivo móvil
    /// </summary>
    public string MobileId { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    public Guid UserGuid { get; set; }

    /// <summary>
    /// Id del dipositivo almacenando en base de datos
    /// </summary>
    public int DeviceId { get; set; }

}