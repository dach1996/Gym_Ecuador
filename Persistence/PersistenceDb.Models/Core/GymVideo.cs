using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Videos de Gimnasio
/// </summary>
[Table(name: "VIDEOS_GIMNASIO", Schema = "GIMNASIO")]
public class GymVideo
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("VGY_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("VGY_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id del gimnasio
    /// </summary>
    [Required]
    [Column("GYM_ID")]
    public int GymId { get; set; }

    /// <summary>
    /// URL del video
    /// </summary>
    [Required]
    [StringLength(500)]
    [Column("VGY_URL_VIDEO")]
    public string VideoUrl { get; set; }

    /// <summary>
    /// Título del video
    /// </summary>
    [Required]
    [StringLength(200)]
    [Column("VGY_TITULO")]
    public string Title { get; set; }

    /// <summary>
    /// Descripción del video
    /// </summary>
    [StringLength(1000)]
    [Column("VGY_DESCRIPCION")]
    public string Description { get; set; }

    /// <summary>
    /// Estado del video
    /// </summary>
    [Required]
    [Column("VGY_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("VGY_FECHA_REGISTRO")]
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
