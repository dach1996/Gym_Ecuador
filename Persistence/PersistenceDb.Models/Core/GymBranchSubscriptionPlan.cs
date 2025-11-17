using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla de Planes de Suscripción
/// Define los diferentes planes de pago que ofrece un gimnasio
/// </summary>
[Table(name: "SUCURSAL_PLAN_SUSCRIPCION", Schema = "CORE")]
public class GymBranchSubscriptionPlan
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("SPS_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("SPS_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id del gimnasio
    /// </summary>
    [Required]
    [Column("SGY_ID")]
    [ForeignKey(nameof(Gym))]
    public int GymId { get; set; }

    /// <summary>
    /// Nombre del plan
    /// Ej: Básico, Premium, VIP, Estudiante, etc.
    /// </summary>
    [Required]
    [StringLength(200)]
    [Column("SPS_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// Código único del plan
    /// </summary>
    [StringLength(50)]
    [Column("SPS_CODIGO")]
    public string Code { get; set; }

    /// <summary>
    /// Descripción del plan
    /// </summary>
    [StringLength(1000)]
    [Column("SPS_DESCRIPCION")]
    public string Description { get; set; }

    /// <summary>
    /// Precio del plan
    /// </summary>
    [Required]
    [Column("SPS_PRECIO")]
    [Precision(18, 2)]
    public decimal Price { get; set; }

    /// <summary>
    /// Duración del plan en días
    /// </summary>
    [Required]
    [Column("SPS_DURACION_DIAS")]
    public int DurationDays { get; set; }

    /// <summary>
    /// Precio de inscripción o setup fee
    /// </summary>
    [Column("SPS_PRECIO_INSCRIPCION")]
    [Precision(18, 2)]
    public decimal? EnrollmentFee { get; set; }

    /// <summary>
    /// Estado del plan
    /// </summary>
    [Required]
    [Column("SPS_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Navegación al gimnasio
    /// </summary>
    public Gym Gym { get; set; }

    /// <summary>
    /// Navegación a las membresías con este plan
    /// </summary>
    public ICollection<Membership> Memberships { get; set; }
}

