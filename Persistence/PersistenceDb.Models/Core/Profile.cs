using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PersistenceDb.Models.Administration;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Models.Enums;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Perfiles nutricionales / fitness
/// </summary>
[Table(name: "PERFILES", Schema = "CORE")]
public class Profile
{
    /// <summary>
    /// Identificador único del perfil
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("PRF_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid del perfil
    /// </summary>
    [Required]
    [Column("PRF_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id de la persona
    /// </summary>
    [Required]
    [Column("PNA_ID")]
    public int PersonId { get; set; }

    /// <summary>
    /// Nombre del perfil
    /// </summary>
    [Required]
    [StringLength(50)]
    [Column("PRF_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// Tipo de perfil (perder/ganar peso)
    /// </summary>
    [Required]
    [Column("PRF_TIPO")]
    public ProfileType Type { get; set; }

    /// <summary>
    /// Id del catálogo de nivel de actividad física
    /// </summary>
    [Required]
    [Column("CAT_ID_NIVEL_ACTIVIDAD")]
    [ForeignKey(nameof(PhysicalActivityCatalog))]
    public int PhysicalActivityCatalogId { get; set; }

    /// <summary>
    /// Altura en centímetros
    /// </summary>
    [Required]
    [Column("PRF_ALTURA")]
    [Precision(5, 2)]
    public decimal Height { get; set; }

    /// <summary>
    /// Id del catálogo de ritmo de progreso
    /// </summary>
    [Required]
    [Column("CAT_ID_RITMO_PROGRESO")]
    [ForeignKey(nameof(ProgressRateCatalog))]
    public int ProgressRateCatalogId { get; set; }

    /// <summary>
    /// Semanas estimadas del plan
    /// </summary>
    [Required]
    [Column("PRF_SEMANAS_ESTIMADAS")]
    public byte EstimatedWeeks { get; set; }

    /// <summary>
    /// Proteína diaria en gramos
    /// </summary>
    [Required]
    [Column("PRF_PROTEINA")]
    public short Protein { get; set; }

    /// <summary>
    /// Carbohidratos diarios en gramos
    /// </summary>
    [Required]
    [Column("PRF_CARBOHIDRATOS")]
    public short Carbohydrates { get; set; }

    /// <summary>
    /// Grasas diarias en gramos
    /// </summary>
    [Required]
    [Column("PRF_GRASAS")]
    public short Fats { get; set; }

    /// <summary>
    /// Indica si el perfil está activo
    /// </summary>
    [Required]
    [Column("PRF_ESTADO")]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("PRF_FECHA_REGISTRO")]
    public DateTime? DateTimeRegister { get; set; }

    /// <summary>
    /// Id de usuario registrador
    /// </summary>
    [Column("USR_ID_REGISTRADOR")]
    public int? UserIdRegister { get; set; }

    /// <summary>
    /// Persona asociada
    /// </summary>
    [ForeignKey(nameof(PersonId))]
    public virtual Person Person { get; set; }

    /// <summary>
    /// Catálogo de nivel de actividad física
    /// </summary>
    public virtual Catalog PhysicalActivityCatalog { get; set; }

    /// <summary>
    /// Catálogo de ritmo de progreso
    /// </summary>
    public virtual Catalog ProgressRateCatalog { get; set; }
}
