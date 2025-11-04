using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Enums;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Gimnasio
/// </summary>
[Table(name: "GIMNASIO", Schema = "CORE")]
public class Gym
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("GYM_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("GYM_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("GYM_FECHA_REGISTRO")]
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Nombre del gimnasio
    /// </summary>
    [Required]
    [StringLength(200)]
    [Column("GYM_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// Descripción del gimnasio
    /// </summary>
    [StringLength(1000)]
    [Column("GYM_DESCRIPCION")]
    public string Description { get; set; }

    /// <summary>
    /// Descripción corta del gimnasio
    /// </summary>
    [StringLength(300)]
    [Column("GYM_DESCRIPCION_CORTA")]
    public string ShortDescription { get; set; }

    /// <summary>
    /// Teléfono del gimnasio
    /// </summary>
    [StringLength(50)]
    [Column("GYM_TELEFONO")]
    public string Phone { get; set; }

    /// <summary>
    /// Email del gimnasio
    /// </summary>
    [StringLength(200)]
    [Column("GYM_EMAIL")]
    public string Email { get; set; }

    /// <summary>
    /// Sitio web del gimnasio
    /// </summary>
    [StringLength(300)]
    [Column("GYM_SITIO_WEB")]
    public string Website { get; set; }

    /// <summary>
    /// Estado del gimnasio
    /// </summary>
    [Required]
    [Column("GYM_ESTADO")]
    public GymStatus IsActive { get; set; }

    /// <summary>
    /// Navegación a los entrenadores del gimnasio
    /// </summary>
    public ICollection<TrainerGym> TrainerGyms { get; set; }

    /// <summary>
    /// Navegación a las sucursales del gimnasio
    /// </summary>
    public ICollection<GymBranch> GymBranches { get; set; }
}
