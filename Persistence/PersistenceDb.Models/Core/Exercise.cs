using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Administration;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Ejercicios
/// </summary>
[Table(name: "EJERCICIOS", Schema = "CORE")]
public class Exercise
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("EJE_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("EJE_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre del ejercicio
    /// </summary>
    [Required]
    [StringLength(200)]
    [Column("EJE_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// Descripción del ejercicio
    /// </summary>
    [StringLength(1000)]
    [Column("EJE_DESCRIPCION")]
    public string Description { get; set; }

    /// <summary>
    /// Instrucciones del ejercicio
    /// </summary>
    [StringLength(2000)]
    [Column("EJE_INSTRUCCIONES")]
    public string Instructions { get; set; }

    /// <summary>
    /// Id de la imagen del ejercicio
    /// </summary>
    [Column("ARG_ID")]
    [ForeignKey(nameof(Image))]
    public int? ImageId { get; set; }

    /// <summary>
    /// Navegación a la imagen del ejercicio
    /// </summary>
    public FilePersistence Image { get; set; }

    /// <summary>
    /// Navegación a los tags del ejercicio
    /// </summary>
    public ICollection<ExerciseTag> ExerciseTags { get; set; }

    /// <summary>
    /// Navegación a las rutinas que contienen este ejercicio
    /// </summary>
    public ICollection<RoutineExercise> RoutineExercises { get; set; }

    /// <summary>
    /// Navegación a los registros de series
    /// </summary>
    public ICollection<SeriesRecord> SeriesRecords { get; set; }
}
