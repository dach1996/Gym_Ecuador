using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PersistenceDb.Models.Administration;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Relación Ejercicios con Tags/Categorías
/// </summary>
[Table(name: "EJERCICIOS_TAGS", Schema = "CORE")]
[PrimaryKey(nameof(ExerciseId), nameof(CatalogId))]
public class ExerciseTag
{
    /// <summary>
    /// Id del ejercicio
    /// </summary>
    [Required]
    [Column("EJE_ID")]
    public int ExerciseId { get; set; }

    /// <summary>
    /// Id del catálogo (tag/categoría)
    /// </summary>
    [Required]
    [Column("CAT_ID")]
    public int CatalogId { get; set; }

    /// <summary>
    /// Navegación al ejercicio
    /// </summary>
    [ForeignKey(nameof(ExerciseId))]
    public Exercise Exercise { get; set; }

    /// <summary>
    /// Navegación al catálogo
    /// </summary>
    [ForeignKey(nameof(CatalogId))]
    public Catalog Catalog { get; set; }
}
