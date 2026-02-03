using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla de Características de Plan
/// Define las características incluidas o excluidas de cada plan
/// </summary>
[Table(name: "CARACTERISTICAS_PLAN", Schema = "CORE")]
public class PlanFeature
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CPL_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Id del plan de sucursal
    /// </summary>
    [Required]
    [Column("PLS_ID")]
    [ForeignKey(nameof(BranchPlan))]
    public int BranchPlanId { get; set; }

    /// <summary>
    /// Descripción de la característica
    /// </summary>
    [Required]
    [StringLength(500)]
    [Column("CPL_DESCRIPCION")]
    public string Description { get; set; }

    /// <summary>
    /// Tipo de característica (1 = Incluido, 2 = Excluido)
    /// </summary>
    [Required]
    [Column("CPL_TIPO")]
    public PlanFeatureType Type { get; set; }

    /// <summary>
    /// Navegación al plan de sucursal
    /// </summary>
    public BranchPlan BranchPlan { get; set; }
}

/// <summary>
/// Tipo de característica de plan
/// </summary>
public enum PlanFeatureType
{
    /// <summary>
    /// Característica incluida en el plan
    /// </summary>
    Included = 1,

    /// <summary>
    /// Característica excluida del plan
    /// </summary>
    Excluded = 2
}
