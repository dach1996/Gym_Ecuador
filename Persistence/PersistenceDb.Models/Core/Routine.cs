using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Authentication;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Rutinas de Ejercicio
/// </summary>
[Table(name: "RUTINAS", Schema = "CORE")]
public class Routine
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("RUT_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("RUT_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre de la rutina
    /// </summary>
    [Required]
    [StringLength(200)]
    [Column("RUT_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// Fecha de creación
    /// </summary>
    [Required]
    [Column("RUT_FECHA_CREACION")]
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Id del usuario propietario de la rutina
    /// </summary>
    [Required]
    [Column("USR_ID")]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    /// <summary>
    /// Id del usuario que creó la rutina
    /// </summary>
    [Required]
    [Column("USR_ID_CREADOR")]
    [ForeignKey(nameof(CreatedUser))]
    public int CreatedUserId { get; set; }

    /// <summary>
    /// Navegación al usuario propietario
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Navegación al usuario creador
    /// </summary>
    public User CreatedUser { get; set; }

    /// <summary>
    /// Navegación a los ejercicios de la rutina
    /// </summary>
    public ICollection<RoutineExercise> RoutineExercises { get; set; }
}
