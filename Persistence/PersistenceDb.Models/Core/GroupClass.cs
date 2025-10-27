using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Clases Grupales
/// </summary>
[Table(name: "CLASES_GRUPALES", Schema = "GIMNASIO")]
public class GroupClass
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CGR_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("CGR_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id del gimnasio
    /// </summary>
    [Required]
    [Column("GYM_ID")]
    public int GymId { get; set; }

    /// <summary>
    /// Id del entrenador
    /// </summary>
    [Required]
    [Column("ENT_ID")]
    public int TrainerId { get; set; }

    /// <summary>
    /// Nombre de la clase
    /// </summary>
    [Required]
    [StringLength(200)]
    [Column("CGR_NOMBRE_CLASE")]
    public string ClassName { get; set; }

    /// <summary>
    /// Descripción de la clase
    /// </summary>
    [StringLength(1000)]
    [Column("CGR_DESCRIPCION_CLASE")]
    public string ClassDescription { get; set; }

    /// <summary>
    /// Duración en minutos
    /// </summary>
    [Required]
    [Column("CGR_DURACION_MINUTOS")]
    public int DurationMinutes { get; set; }

    /// <summary>
    /// Capacidad máxima
    /// </summary>
    [Required]
    [Column("CGR_CAPACIDAD_MAXIMA")]
    public int MaxCapacity { get; set; }

    /// <summary>
    /// URL de imagen de la clase
    /// </summary>
    [StringLength(500)]
    [Column("CGR_URL_IMAGEN_CLASE")]
    public string ClassImageUrl { get; set; }

    /// <summary>
    /// Estado de la clase
    /// </summary>
    [Required]
    [Column("CGR_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("CGR_FECHA_REGISTRO")]
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

    /// <summary>
    /// Navegación al entrenador
    /// </summary>
    [ForeignKey("TrainerId")]
    public virtual Trainer Trainer { get; set; }
}
