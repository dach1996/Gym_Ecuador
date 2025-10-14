using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Enums;

namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla de notificaciones Push
/// </summary>
[Table(name: "NOTIFICACION_PUSH", Schema = "ADMINISTRACION")]
public class NotificationPush
{
    /// <summary>
    /// Identificador de mensaje push
    /// </summary>
    /// <value></value>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("NCP_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Required]
    [Column("NCP_FECHA_REGISTRO")]
    public DateTime RegisterDate { get; set; }

    /// <summary>
    /// Titulo
    /// </summary>
    [Required]
    [Column("NCP_TITULO")]
    [StringLength(100)]
    public string Title { get; set; }

    /// <summary>
    /// Descripción del mensaje
    /// </summary>
    [Required]
    [Column("NCP_DESCRIPCION")]
    [StringLength(250)]
    public string Description { get; set; }

    /// <summary>
    /// Tipo de notificación push
    /// </summary>
    [Required]
    [Column("NCP_CODIGO_TIPO_NOTIFICACION")]
    public PushNotificationType PushNotificationType { get; set; }

    /// <summary>
    /// Valor de tipo de notificación Topico o Token
    /// </summary>
    [Required]
    [Column("NCP_VALOR_TIPO_NOTIFICACION")]
    public string PushNotificationValue { get; set; }

    /// <summary>
    /// Categoría de envío de notificación
    /// </summary>
    [Column("NCP_PERMITIR_VER_USUARIO")]
    [Required]
    public bool AllowViewUser { get; set; }

    /// <summary>
    /// Lista de notificaciones Push
    /// </summary>
    public ICollection<NotificationPushUser> NotificationsPushUser { get; set; }
}
