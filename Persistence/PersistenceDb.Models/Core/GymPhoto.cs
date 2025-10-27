using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Fotos de Gimnasio
/// </summary>
[Table(name: "FOTOS_GIMNASIO", Schema = "GIMNASIO")]
public class GymPhoto
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("FGY_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("FGY_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id del gimnasio
    /// </summary>
    [Required]
    [Column("GYM_ID")]
    public int GymId { get; set; }

    /// <summary>
    /// URL de la imagen
    /// </summary>
    [Required]
    [StringLength(500)]
    [Column("FGY_URL_IMAGEN")]
    public string ImageUrl { get; set; }

    /// <summary>
    /// Descripción de la imagen
    /// </summary>
    [StringLength(300)]
    [Column("FGY_DESCRIPCION")]
    public string Description { get; set; }

    /// <summary>
    /// Es imagen principal
    /// </summary>
    [Required]
    [Column("FGY_ES_PRINCIPAL")]
    public bool IsMain { get; set; }

    /// <summary>
    /// Estado de la foto
    /// </summary>
    [Required]
    [Column("FGY_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("FGY_FECHA_REGISTRO")]
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
