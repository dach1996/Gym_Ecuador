using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Valores de parámetros físicos por registro de seguimiento de proceso
/// </summary>
[Table(name: "SEGUIMIENTO_PROCESOS_MEDIDAS", Schema = "CORE")]
public class ProcessTrackingMeasurement
{
    /// <summary>
    /// Identificador único del valor de medida
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("SPM_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Identificador del seguimiento de proceso
    /// </summary>
    [Required]
    [Column("SPR_ID")]
    [ForeignKey(nameof(ProcessTracking))]
    public int ProcessTrackingId { get; set; }

    /// <summary>
    /// Identificador del parámetro físico
    /// </summary>
    [Required]
    [Column("PAF_ID")]
    [ForeignKey(nameof(PhysicalParameter))]
    public byte PhysicalParameterId { get; set; }

    /// <summary>
    /// Valor registrado del parámetro
    /// </summary>
    [Required]
    [Column("SPM_VALOR")]
    [Precision(5, 2)]
    public decimal Value { get; set; }

    /// <summary>
    /// Navegación al seguimiento de proceso
    /// </summary>
    public ProcessTracking ProcessTracking { get; set; }

    /// <summary>
    /// Navegación al parámetro físico
    /// </summary>
    public PhysicalParameter PhysicalParameter { get; set; }
}
