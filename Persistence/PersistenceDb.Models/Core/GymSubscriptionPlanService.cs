using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla de relación Servicio-Plan de Suscripción de Sucursal de Gimnasio
/// Asocia servicios con planes de suscripción de sucursales y define características específicas por sucursal
/// </summary>
[Table(name: "SUCURSAL_PLAN_SERVICIO", Schema = "CORE")]
public class GymSubscriptionPlanService
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("SGS_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Id del servicio
    /// </summary>
    [Required]
    [Column("SER_ID")]
    [ForeignKey(nameof(Service))]
    public int ServiceId { get; set; }

    /// <summary>
    /// Id del plan de suscripción
    /// </summary>
    [Required]
    [Column("PLS_ID")]
    [ForeignKey(nameof(GymBranchSubscriptionPlan))]
    public int GymBranchSubscriptionPlanId { get; set; }

    /// <summary>
    /// Navegación al servicio
    /// </summary>
    [ForeignKey(nameof(ServiceId))]
    public virtual Service Service { get; set; }
}

