using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla de relación Entrenador - Gimnasio
/// </summary>
[Table(name: "ENTRENADOR_GIMNASIO", Schema = "CORE")]
public class TrainerGym
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("EGY_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Id del entrenador
    /// </summary>
    [Required]
    [Column("ENT_ID")]
    public int TrainerId { get; set; }

    /// <summary>
    /// Id del gimnasio
    /// </summary>
    [Required]
    [Column("GYM_ID")]
    public int GymId { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("EGY_FECHA_REGISTRO")]
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Estado de la relación
    /// </summary>
    [Required]
    [Column("EGY_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Navegación al entrenador
    /// </summary>
    [ForeignKey(nameof(TrainerId))]
    public Trainer Trainer { get; set; }

    /// <summary>
    /// Navegación al gimnasio
    /// </summary>
    [ForeignKey(nameof(GymId))]
    public Gym Gym { get; set; }
}

