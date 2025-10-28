using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Authentication;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Membresías
/// </summary>
[Table(name: "MEMBRESIAS", Schema = "CORE")]
public class Membership
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
    /// Id de la persona
    /// </summary>
    [Required]
    [Column("PNA_ID")]
    public int PersonId { get; set; }

    /// <summary>
    /// Id del gimnasio
    /// </summary>
    [Required]
    [Column("GYM_ID")]
    public int GymId { get; set; }

    /// <summary>
    /// Id del tipo de membresía
    /// </summary>
    [Required]
    [Column("TMP_ID")]
    public int MembershipTypeId { get; set; }

    /// <summary>
    /// Fecha de inicio
    /// </summary>
    [Required]
    [Column("MEM_FECHA_INICIO")]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de fin
    /// </summary>
    [Required]
    [Column("MEM_FECHA_FIN")]
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Estado de la membresía
    /// </summary>
    [Required]
    [StringLength(50)]
    [Column("MEM_ESTADO")]
    public string Status { get; set; } // Activa, Vencida, Congelada

    /// <summary>
    /// Rol en el gimnasio
    /// </summary>
    [Required]
    [StringLength(50)]
    [Column("MEM_ROL_GIMNASIO")]
    public string GymRole { get; set; } // Miembro, Entrenador, Administrador

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("MEM_FECHA_REGISTRO")]
    public DateTime? DateTimeRegister { get; set; }

    /// <summary>
    /// Id de usuario registrador
    /// </summary>
    [Column("USR_ID_REGISTRADOR")]
    public int? UserIdRegister { get; set; }

    /// <summary>
    /// Navegación a la persona
    /// </summary>
    [ForeignKey("PersonId")]
    public virtual Person Person { get; set; }

    /// <summary>
    /// Navegación al gimnasio
    /// </summary>
    [ForeignKey("GymId")]
    public virtual Gym Gym { get; set; }

    /// <summary>
    /// Navegación al tipo de membresía
    /// </summary>
    [ForeignKey("MembershipTypeId")]
    public virtual MembershipType MembershipType { get; set; }
}
