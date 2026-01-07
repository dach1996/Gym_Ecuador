
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla de Planes de Sucursal
/// Define los diferentes planes de pago que ofrece una sucursal de gimnasio
/// </summary>
[Table(name: "PLAN_SUCURSAL", Schema = "CORE")]
public class BranchPlan
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("PLS_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("PLS_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id de la sucursal de gimnasio
    /// </summary>
    [Required]
    [Column("SGY_ID")]
    [ForeignKey(nameof(GymBranch))]
    public int GymBranchId { get; set; }

    /// <summary>
    /// Nombre del plan
    /// Ej: Básico, Premium, VIP, Estudiante, etc.
    /// </summary>
    [Required]
    [StringLength(200)]
    [Column("PLS_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// Código único del plan
    /// </summary>
    [StringLength(50)]
    [Column("PLS_CODIGO")]
    public string Code { get; set; }

    /// <summary>
    /// Descripción del plan
    /// </summary>
    [StringLength(1000)]
    [Column("PLS_DESCRIPCION")]
    public string Description { get; set; }

    /// <summary>
    /// Precio del plan
    /// </summary>
    [Required]
    [Column("PLS_PRECIO")]
    [Precision(18, 2)]
    public decimal Price { get; set; }

    /// <summary>
    /// Duración del plan en días
    /// </summary>
    [Required]
    [Column("PLS_DURACION_DIAS")]
    public int DurationDays { get; set; }

    /// <summary>
    /// Precio de inscripción o setup fee
    /// </summary>
    [Column("PLS_PRECIO_INSCRIPCION")]
    [Precision(18, 2)]
    public decimal? EnrollmentFee { get; set; }

    /// <summary>
    /// Estado del plan
    /// </summary>
    [Required]
    [Column("PLS_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Navegación a la sucursal de gimnasio
    /// </summary>
    public GymBranch GymBranch { get; set; }

    /// <summary>
    /// Navegación a las membresías de clientes con este plan
    /// </summary>
    public ICollection<ClientMembership> ClientMemberships { get; set; }
}

