using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Authentication;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Reseñas de Gimnasio
/// </summary>
[Table(name: "RESENAS_GIMNASIO", Schema = "CORE")]
public class GymReview
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("RGY_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("RGY_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id del gimnasio
    /// </summary>
    [Required]
    [Column("GYM_ID")]
    public int GymId { get; set; }

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
    [Column("RGY_CALIFICACION")]
    public int Rating { get; set; }

    /// <summary>
    /// Comentario
    /// </summary>
    [StringLength(2000)]
    [Column("RGY_COMENTARIO")]
    public string Comment { get; set; }

    /// <summary>
    /// Fecha de reseña
    /// </summary>
    [Required]
    [Column("RGY_FECHA_RESENA")]
    public DateTime ReviewDate { get; set; }

    /// <summary>
    /// Estado de la reseña
    /// </summary>
    [Required]
    [Column("RGY_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("RGY_FECHA_REGISTRO")]
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
    /// Navegación a la persona
    /// </summary>
    [ForeignKey("PersonId")]
    public virtual Person Person { get; set; }
}
