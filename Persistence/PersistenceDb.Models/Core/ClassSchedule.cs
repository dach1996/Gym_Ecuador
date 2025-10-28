using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Horarios de Clase
/// </summary>
[Table(name: "HORARIOS_CLASE", Schema = "CORE")]
public class ClassSchedule
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("HCL_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("HCL_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id de la clase grupal
    /// </summary>
    [Required]
    [Column("CGR_ID")]
    public int GroupClassId { get; set; }

    /// <summary>
    /// Día de la semana
    /// </summary>
    [Required]
    [StringLength(20)]
    [Column("HCL_DIA_SEMANA")]
    public string DayOfWeek { get; set; }

    /// <summary>
    /// Hora de inicio
    /// </summary>
    [Required]
    [Column("HCL_HORA_INICIO")]
    public TimeSpan StartTime { get; set; }

    /// <summary>
    /// Hora de fin
    /// </summary>
    [Required]
    [Column("HCL_HORA_FIN")]
    public TimeSpan EndTime { get; set; }

    /// <summary>
    /// Ubicación de la sala
    /// </summary>
    [StringLength(100)]
    [Column("HCL_UBICACION_SALA")]
    public string RoomLocation { get; set; }

    /// <summary>
    /// Estado del horario
    /// </summary>
    [Required]
    [Column("HCL_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("HCL_FECHA_REGISTRO")]
    public DateTime? DateTimeRegister { get; set; }

    /// <summary>
    /// Id de usuario registrador
    /// </summary>
    [Column("USR_ID_REGISTRADOR")]
    public int? UserIdRegister { get; set; }

    /// <summary>
    /// Navegación a la clase grupal
    /// </summary>
    [ForeignKey("GroupClassId")]
    public virtual GroupClass GroupClass { get; set; }
}
