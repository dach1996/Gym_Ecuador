using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Enums;
namespace PersistenceDb.Models.Authentication;
/// <summary>
/// Tabla Usuario
/// </summary>
[Table(name: "DISPOSITIVO", Schema = "AUTENTICACION")]
public class Device
{
    /// <summary>
    /// Identificador de dispositivo
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("DIS_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Plataforma del dispositivo
    /// </summary>
    [Required]
    [Column("DIS_CODIGO_PLATAFORMA")]
    public PlatformType Platform { get; set; }

    /// <summary>
    /// Identificador del móvil generado en el móvil
    /// </summary>
    [Required]
    [StringLength(100)]
    [Column("DIS_ID_DISPOSITIVO")]
    public string MobileId { get; set; }

    /// <summary>
    /// Modelo de dispositivo
    /// </summary>
    [Required]
    [StringLength(100)]
    [Column("DIS_MODELO")]
    public string Model { get; set; }

    /// <summary>
    /// Marca del dispositivo
    /// </summary>
    [Required]
    [StringLength(50)]
    [Column("DIS_MARCA")]
    public string Brand { get; set; }

    /// <summary>
    /// Sistema Operativo
    /// </summary>
    [Required]
    [StringLength(50)]
    [Column("DIS_SISTEMA_OPERATIVO")]
    public string SystemOperation { get; set; }

    /// <summary>
    /// Tiene Bloqueo
    /// </summary>
    [Required]
    [Column("DIS_TIENE_BLOQUEO")]
    public bool HasBlock { get; set; }

    /// <summary>
    /// Tiene Servicios de Google
    /// </summary>
    [Required]
    [Column("DIS_TIENE_SERVICIO_GOOGLE")]
    public bool HasGoogleServices { get; set; }

    /// <summary>
    /// Lista de Usuarios relacinados con dispositivos
    /// </summary>
    /// <value></value>
    public ICollection<UserDevice> UserDevices { get; set; }

    /// <summary>
    /// Token de Notificación del dispositivo
    /// </summary>
    /// <value></value>
    public UserDevicePushToken UserDevicePushToken { get; set; }
}
