using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Authentication;
namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla de notificaciones Push enlazada a los usuarios enviados
/// </summary>
[Table(name: "NOTIFICACION_PUSH_USUARIO", Schema = "ADMINISTRACION")]
public class NotificationPushUser
{

    /// <summary>
    /// Identificador de mensaje push
    /// </summary>
    /// <value></value>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("NPU_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Id de Usuario
    /// </summary>
    /// <value></value>
    [Column("USR_ID")]
    [Required]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    /// <summary>
    /// Id de notificación Push
    /// </summary>
    /// <value></value>
    [Column("NCP_ID")]
    [Required]
    [ForeignKey(nameof(NotificationPush))]
    public int PushNotificationId { get; set; }

    /// <summary>
    /// Verifica si el usuario ha visto la Notificación
    /// </summary>
    [Column("NPU_TIENE_VISTA")]
    [Required]
    public bool HasRead { get; set; }


    /// <summary>
    /// Usuario
    /// </summary>
    public User User { get; set; }

    /// <summary>
    ///Usuario
    /// </summary>
    public NotificationPush NotificationPush { get; set; }

    /// <summary>
    /// Dispositivos de la notificacion
    /// </summary>
    public ICollection<NotificationPushUserDevice> NotificationPushUserDevices { get; set; }
}
