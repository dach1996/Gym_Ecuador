using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Models.Enums;
namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla de notificaciones Push enlazada a los dispositivos  de usuarios enviados
/// </summary>
[Table(name: "NOTIFICACION_PUSH_USUARIO_DISPOSITIVO", Schema = "ADMINISTRACION")]
[PrimaryKey(nameof(NotificationPushUserId), nameof(DeviceId))]
public class NotificationPushUserDevice
{
    /// <summary>
    /// Identificador de mensaje push
    /// </summary>
    /// <value></value>
    [ForeignKey(nameof(NotificationPushUser))]
    [Required]
    [Column("NPU_ID")]
    public int NotificationPushUserId { get; set; }

    /// <summary>
    /// Id de Usuario
    /// </summary>
    /// <value></value>
    [Column("DIS_ID")]
    [ForeignKey(nameof(Device))]
    [Required]
    public int DeviceId { get; set; }


    /// <summary>
    /// Estado de la Notificación
    /// </summary>
    [Required]
    [Column("PUD_ESTADO")]
    public StatusNotification StatusNotification { get; set; }

    /// <summary>
    /// Estado de la Notificación
    /// </summary>
    [Column("PUD_INFORMACION_ADICIONAL")]
    public string AdditionalInformation { get; set; }

    /// <summary>
    /// Estado no Mapeado
    /// </summary>
    /// <value></value>
    [NotMapped]
    public bool IsSuccess { get => StatusNotification == StatusNotification.Sended; }

    /// <summary>
    /// Dispositivo
    /// </summary>
    public Device Device { get; set; }

    /// <summary>
    ///Usuario de la notificacion
    /// </summary>
    public NotificationPushUser NotificationPushUser { get; set; }
}
