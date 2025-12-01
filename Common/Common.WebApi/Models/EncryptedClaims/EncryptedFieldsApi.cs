
using Common.WebCommon.Models;

namespace Common.WebApi.Models.EncryptedClaims;

public class EncryptedFieldsApi : EncryptedFieldClaimCommon
{

    /// <summary>
    /// Id de usuario 
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Id de persona
    /// </summary>
    public int? PersonId { get; set; }

    /// <summary>
    /// Identificador del dispositivo generado por el dispositivo móvil
    /// </summary>
    public string MobileId { get; set; }

    /// <summary>
    /// Id del dipositivo almacenando en base de datos
    /// </summary>
    public int DeviceId { get; set; }

    /// <summary>
    /// Fecha de Registro de Usuario
    /// </summary>
    public DateTime UserDateTimeRegister { get; set; }
}