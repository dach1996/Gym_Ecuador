using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Tipos de Membresía
/// </summary>
[Table(name: "TIPOS_MEMBRESIA", Schema = "GIMNASIO")]
public class MembershipType
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("TMP_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("TMP_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id del gimnasio
    /// </summary>
    [Required]
    [Column("GYM_ID")]
    public int GymId { get; set; }

    /// <summary>
    /// Nombre del plan
    /// </summary>
    [Required]
    [StringLength(150)]
    [Column("TMP_NOMBRE_PLAN")]
    public string PlanName { get; set; }

    /// <summary>
    /// Precio del plan
    /// </summary>
    [Required]
    [Column("TMP_PRECIO")]
    public decimal Price { get; set; }

    /// <summary>
    /// Duración en días
    /// </summary>
    [Required]
    [Column("TMP_DURACION_DIAS")]
    public int DurationDays { get; set; }

    /// <summary>
    /// Descripción de beneficios
    /// </summary>
    [StringLength(1000)]
    [Column("TMP_DESCRIPCION_BENEFICIOS")]
    public string BenefitsDescription { get; set; }

    /// <summary>
    /// Estado del tipo de membresía
    /// </summary>
    [Required]
    [Column("TMP_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("TMP_FECHA_REGISTRO")]
    public DateTime? DateTimeRegister { get; set; }

    /// <summary>
    /// Id de usuario registrador
    /// </summary>
    [Column("USR_ID_REGISTRADOR")]
    public int? UserIdRegister { get; set; }

    /// <summary>
    /// Navegación al gimnasio
    /// </summary>
    [ForeignKey("GymId")]
    public virtual Gym Gym { get; set; }
}
