using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Relación Rutinas con Ejercicios
/// </summary>
[Table(name: "RUTINA_EJERCICIO", Schema = "CORE")]
public class RoutineExercise
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("RUE_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Id de la rutina
    /// </summary>
    [Required]
    [Column("RUT_ID")]
    [ForeignKey(nameof(Routine))]
    public int RoutineId { get; set; }

    /// <summary>
    /// Id del ejercicio
    /// </summary>
    [Required]
    [Column("EJE_ID")]
    [ForeignKey(nameof(Exercise))]
    public int ExerciseId { get; set; }

    /// <summary>
    /// Número de series
    /// </summary>
    [Required]
    [Column("RUE_SERIES")]
    public int Series { get; set; }

    /// <summary>
    /// Desde cuántas repeticiones
    /// </summary>
    [Required]
    [Column("RUE_REPETICIONES_DESDE")]
    public int RepetitionsFrom { get; set; }

    /// <summary>
    /// Hasta cuántas repeticiones
    /// </summary>
    [Required]
    [Column("RUE_REPETICIONES_HASTA")]
    public int RepetitionsTo { get; set; }

    /// <summary>
    /// Segundos de descanso
    /// </summary>
    [Required]
    [Column("RUE_SEGUNDOS_DESCANSO")]
    public int RestSeconds { get; set; }

    /// <summary>
    /// Día de la semana en que se debe realizar el ejercicio (1-7, donde 1=Lunes, 7=Domingo)
    /// </summary>
    [Required]
    [Column("RUE_DIA")]
    public byte Day { get; set; }

    /// <summary>
    /// Navegación a la rutina
    /// </summary>
    public Routine Routine { get; set; }

    /// <summary>
    /// Navegación al ejercicio
    /// </summary>
    public Exercise Exercise { get; set; }
}
