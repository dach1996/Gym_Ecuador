using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Authentication;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Calificaciones de Entrenador
/// </summary>
[Table(name: "CALIFICACIONES_ENTRENADOR", Schema = "CORE")]
public class TrainerRating
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CEN_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("CEN_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id del entrenador
    /// </summary>
    [Required]
    [Column("ENT_ID")]
    public int TrainerId { get; set; }

    /// <summary>
    /// Id de la persona
    /// </summary>
    [Required]
    [Column("PNA_ID")]
    public int PersonId { get; set; }

    /// <summary>
    /// Calificación (1-5 estrellas)
    /// </summary>
    [Required]
    [Range(1, 5)]
    [Column("CEN_CALIFICACION")]
    public int Rating { get; set; }

    /// <summary>
    /// Comentario
    /// </summary>
    [StringLength(2000)]
    [Column("CEN_COMENTARIO")]
    public string Comment { get; set; }

    /// <summary>
    /// Fecha de calificación
    /// </summary>
    [Required]
    [Column("CEN_FECHA_CALIFICACION")]
    public DateTime RatingDate { get; set; }

    /// <summary>
    /// Estado de la calificación
    /// </summary>
    [Required]
    [Column("CEN_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("CEN_FECHA_REGISTRO")]
    public DateTime? DateTimeRegister { get; set; }

    /// <summary>
    /// Id de usuario registrador
    /// </summary>
    [Column("USR_ID_REGISTRADOR")]
    public int? UserIdRegister { get; set; }

    /// <summary>
    /// Navegación al entrenador
    /// </summary>
    [ForeignKey("TrainerId")]
    public virtual Trainer Trainer { get; set; }

    /// <summary>
    /// Navegación a la persona
    /// </summary>
    [ForeignKey("PersonId")]
    public virtual Person Person { get; set; }
}
