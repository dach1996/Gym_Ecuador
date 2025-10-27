using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Gimnasio
/// </summary>
[Table(name: "GIMNASIO", Schema = "GIMNASIO")]
public class Gym
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("GYM_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("GYM_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre del gimnasio
    /// </summary>
    [Required]
    [StringLength(200)]
    [Column("GYM_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// Descripción del gimnasio
    /// </summary>
    [StringLength(1000)]
    [Column("GYM_DESCRIPCION")]
    public string Description { get; set; }

    /// <summary>
    /// Descripción corta del gimnasio
    /// </summary>
    [StringLength(300)]
    [Column("GYM_DESCRIPCION_CORTA")]
    public string ShortDescription { get; set; }

    /// <summary>
    /// Dirección del gimnasio
    /// </summary>
    [StringLength(500)]
    [Column("GYM_DIRECCION")]
    public string Address { get; set; }

    /// <summary>
    /// Teléfono del gimnasio
    /// </summary>
    [StringLength(50)]
    [Column("GYM_TELEFONO")]
    public string Phone { get; set; }

    /// <summary>
    /// Email del gimnasio
    /// </summary>
    [StringLength(200)]
    [Column("GYM_EMAIL")]
    public string Email { get; set; }

    /// <summary>
    /// Sitio web del gimnasio
    /// </summary>
    [StringLength(300)]
    [Column("GYM_SITIO_WEB")]
    public string Website { get; set; }

    /// <summary>
    /// Horario de apertura
    /// </summary>
    [Column("GYM_HORARIO_APERTURA")]
    public TimeSpan? OpeningTime { get; set; }

    /// <summary>
    /// Horario de cierre
    /// </summary>
    [Column("GYM_HORARIO_CIERRE")]
    public TimeSpan? ClosingTime { get; set; }

    /// <summary>
    /// Latitud para localización
    /// </summary>
    [Column("GYM_LATITUD")]
    public decimal? Latitude { get; set; }

    /// <summary>
    /// Longitud para localización
    /// </summary>
    [Column("GYM_LONGITUD")]
    public decimal? Longitude { get; set; }

    /// <summary>
    /// Estado del gimnasio
    /// </summary>
    [Required]
    [Column("GYM_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("GYM_FECHA_REGISTRO")]
    public DateTime? DateTimeRegister { get; set; }

    /// <summary>
    /// Id de usuario registrador
    /// </summary>
    [Column("USR_ID_REGISTRADOR")]
    public int? UserIdRegister { get; set; }
}
