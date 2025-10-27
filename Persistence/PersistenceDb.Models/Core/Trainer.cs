using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Authentication;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Entrenadores
/// </summary>
[Table(name: "ENTRENADORES", Schema = "GIMNASIO")]
public class Trainer
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ENT_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("ENT_GUID")]
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
    /// Especialidad del entrenador
    /// </summary>
    [StringLength(200)]
    [Column("ENT_ESPECIALIDAD")]
    public string Specialty { get; set; }

    /// <summary>
    /// Biografía del entrenador
    /// </summary>
    [StringLength(2000)]
    [Column("ENT_BIOGRAFIA")]
    public string Biography { get; set; }

    /// <summary>
    /// URL de foto de perfil
    /// </summary>
    [StringLength(500)]
    [Column("ENT_URL_FOTO_PERFIL")]
    public string ProfilePhotoUrl { get; set; }

    /// <summary>
    /// Estado del entrenador
    /// </summary>
    [Required]
    [Column("ENT_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("ENT_FECHA_REGISTRO")]
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
}
