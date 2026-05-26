using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Catálogo de parámetros físicos (peso, medidas, porcentajes)
/// </summary>
[Table(name: "PARAMETROS_FISICOS", Schema = "CORE")]
public class PhysicalParameter
{
    /// <summary>
    /// Identificador único del parámetro físico
    /// </summary>
    [Key]
    [Required]
    [Column("PAF_ID")]
    public byte Id { get; set; }

    /// <summary>
    /// Código único del parámetro (ej. PESO, ALTURA)
    /// </summary>
    [Required]
    [StringLength(64)]
    [Column("PAF_CODIGO")]
    public string Code { get; set; }

    /// <summary>
    /// Nombre descriptivo del parámetro
    /// </summary>
    [Required]
    [StringLength(128)]
    [Column("PAF_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// Estado del parámetro (activo/inactivo)
    /// </summary>
    [Required]
    [Column("PAF_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Valores registrados en seguimientos de proceso
    /// </summary>
    public virtual ICollection<ProcessTrackingMeasurement> ProcessTrackingMeasurements { get; set; }
}
