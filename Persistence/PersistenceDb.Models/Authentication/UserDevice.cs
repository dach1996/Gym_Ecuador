using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PersistenceDb.Models.Attributes;

namespace PersistenceDb.Models.Authentication;
/// <summary>
/// Tabla Usuario Dipositivo
/// </summary>
[Table(name: "USUARIO_DISPOSITIVO", Schema = "AUTENTICACION")]
[PrimaryKey(nameof(UserId), nameof(DeviceId))]
public class UserDevice
{
    /// <summary>
    /// Id de Usuario
    /// </summary>
    /// <value></value>
    [Column("USR_ID")]
    [ForeignKey(nameof(User))]
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// Id de Dispositivo
    /// </summary>
    /// <value></value>
    [Column("DIS_ID")]
    [ForeignKey(nameof(Device))]
    [Required]
    public int DeviceId { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Required]
    [Column("UDR_FECHA_REGISTRO")]
    public DateTime RegisterDate { get; set; }

    /// <summary>
    /// Fecha de último ingreso
    /// </summary>
    [Required]
    [Column("UDR_FECHA_ULTIMO_INGRESO")]
    public DateTime DateTimeLastLogin { get; set; }

    /// <summary>
    /// Biométrico registrado
    /// </summary>
    [Column("UDR_BIOMETRICO")]
    [EncryptColumn]
    public string Biometric { get; set; }

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
