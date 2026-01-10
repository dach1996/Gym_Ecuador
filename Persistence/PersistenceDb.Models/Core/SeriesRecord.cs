using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PersistenceDb.Models.Authentication;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Registro de Series
/// </summary>
[Table(name: "REGISTRO_SERIES", Schema = "CORE")]
public class SeriesRecord
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("SER_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("SER_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id del ejercicio
    /// </summary>
    [Required]
    [Column("EJE_ID")]
    [ForeignKey(nameof(Exercise))]
    public int ExerciseId { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Required]
    [Column("SER_FECHA_REGISTRO")]
    public DateTime RegistrationDate { get; set; }

    /// <summary>
    /// Id del usuario
    /// </summary>
    [Required]
    [Column("USR_ID")]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    /// <summary>
    /// Peso utilizado
    /// </summary>
    [Column("SER_PESO")]
    [Precision(5, 2)]
    public decimal? Weight { get; set; }

    /// <summary>
    /// Repeticiones realizadas
    /// </summary>
    [Required]
    [Column("SER_REPETICIONES")]
    public int Repetitions { get; set; }

    /// <summary>
    /// Navegación al ejercicio
    /// </summary>
    public Exercise Exercise { get; set; }

    /// <summary>
    /// Navegación al usuario
    /// </summary>
    public User User { get; set; }
}
