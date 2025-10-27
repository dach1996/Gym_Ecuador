using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Máquinas de Gimnasio
/// </summary>
[Table(name: "MAQUINAS_GIMNASIO", Schema = "GIMNASIO")]
public class GymMachine
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("MGY_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("MGY_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id del gimnasio
    /// </summary>
    [Required]
    [Column("GYM_ID")]
    public int GymId { get; set; }

    /// <summary>
    /// Nombre de la máquina
    /// </summary>
    [Required]
    [StringLength(200)]
    [Column("MGY_NOMBRE_MAQUINA")]
    public string MachineName { get; set; }

    /// <summary>
    /// Tipo de máquina
    /// </summary>
    [Required]
    [StringLength(100)]
    [Column("MGY_TIPO_MAQUINA")]
    public string MachineType { get; set; }

    /// <summary>
    /// URL de imagen de la máquina
    /// </summary>
    [StringLength(500)]
    [Column("MGY_URL_IMAGEN_MAQUINA")]
    public string MachineImageUrl { get; set; }

    /// <summary>
    /// Estado de la máquina
    /// </summary>
    [Required]
    [StringLength(50)]
    [Column("MGY_ESTADO_MAQUINA")]
    public string MachineStatus { get; set; } // Disponible, En Mantenimiento

    /// <summary>
    /// Última revisión
    /// </summary>
    [Column("MGY_ULTIMA_REVISION")]
    public DateTime? LastRevision { get; set; }

    /// <summary>
    /// Estado del registro
    /// </summary>
    [Required]
    [Column("MGY_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("MGY_FECHA_REGISTRO")]
    public DateTime? DateTimeRegister { get; set; }

    /// <summary>
    /// Id de usuario registrador
    /// </summary>
    [Column("USR_ID_REGISTRADOR")]
    public int? UserIdRegister { get; set; }

    /// <summary>
    /// Navegación al gimnasio
    /// </summary>
    [ForeignKey("GymId")]
    public virtual Gym Gym { get; set; }
}
