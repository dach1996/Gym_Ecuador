using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PersistenceDb.Models.Enums;

namespace PersistenceDb.Models.Authentication;
/// <summary>
/// Tabla Usuario Dipositivo Notifación Push
/// </summary>
[Table(name: "USUARIO_DISPOSITIVO_PUSH_TOKEN", Schema = "AUTENTICACION")]
[PrimaryKey(nameof(DeviceId))]
public class UserDevicePushToken
{
    /// <summary>
    /// Id de Dispositivo
    /// </summary>
    /// <value></value>
    [Column("DIS_ID")]
    [ForeignKey(nameof(Device))]
    public int DeviceId { get; set; }

    /// <summary>
    /// Id de Usuario
    /// </summary>
    /// <value></value>
    [Column("USR_ID")]
    [ForeignKey(nameof(User))]
    [Required]
    public int? UserId { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Required]
    [Column("UDP_FECHA_ACTUALIZACION")]
    public DateTime LastDateUpdated { get; set; }

    /// <summary>
    /// Push Token Registrado
    /// </summary>
    [Required]
    [Column("UDP_PUSH_TOKEN")]
    public string PushToken { get; set; }

    /// <summary>
    /// código de implementación de envío
    /// </summary>
    [Required]
    [Column("UDP_CODIGO_IMPLEMENTACION")]
    public NotificationPushImplementationType NotificationPushImplementationType { get; set; }

    /// <summary>
    /// Usuario
    /// </summary>
    /// <value></value>
    public User User { get; set; }

    /// <summary>
    /// Dispositivo
    /// </summary>
    /// <value></value>
    public Device Device { get; set; }
}
