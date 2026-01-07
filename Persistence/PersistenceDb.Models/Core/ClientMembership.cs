using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Membrecías
/// Relaciona clientes de sucursal de gimnasio con planes de suscripción
/// </summary>
[Table(name: "MEMBRECIAS", Schema = "CORE")]
public class ClientMembership
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("MEM_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("MEM_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id del cliente (ClienteSucursalGimnacio)
    /// </summary>
    [Required]
    [Column("CSG_ID")]
    [ForeignKey(nameof(ClientGymBranch))]
    public int ClientGymBranchId { get; set; }

    /// <summary>
    /// Id del plan
    /// </summary>
    [Required]
    [Column("PLS_ID")]
    [ForeignKey(nameof(BranchPlan))]
    public int BranchPlanId { get; set; }

    /// <summary>
    /// Fecha de inicio de la membresía
    /// </summary>
    [Required]
    [Column("MEM_FECHA_INICIO")]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de fin de la membresía
    /// </summary>
    [Column("MEM_FECHA_FIN")]
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Estado de la membresía
    /// </summary>
    [Required]
    [Column("MEM_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Required]
    [Column("MEM_FECHA_REGISTRO")]
    public DateTime RegistrationDate { get; set; }

    /// <summary>
    /// Navegación al cliente de sucursal de gimnasio
    /// </summary>
    public ClientGymBranch ClientGymBranch { get; set; }

    /// <summary>
    /// Navegación al plan
    /// </summary>
    public BranchPlan BranchPlan { get; set; }
}

