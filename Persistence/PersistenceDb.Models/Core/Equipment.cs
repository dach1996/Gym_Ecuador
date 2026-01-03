using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Administration;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Equipamientos de Sucursal de Gimnasio
/// </summary>
[Table(name: "EQUIPAMIENTO", Schema = "CORE")]
public class Equipment
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("EQP_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("EQP_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id de la sucursal de gimnasio
    /// </summary>
    [Required]
    [Column("SGY_ID")]
    [ForeignKey(nameof(GymBranch))]
    public int GymBranchId { get; set; }

    /// <summary>
    /// Nombre del equipamiento
    /// </summary>
    [Required]
    [StringLength(200)]
    [Column("EQP_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// Descripción del equipamiento
    /// </summary>
    [StringLength(1000)]
    [Column("EQP_DESCRIPCION")]
    public string Description { get; set; }

    /// <summary>
    /// Tipo de equipamiento (Máquina, Pesas, Accesorios, etc.)
    /// </summary>
    [Required]
    [StringLength(100)]
    [Column("EQP_TIPO")]
    [ForeignKey(nameof(EquipmentTypeCatalog))]
    public int EquipmentTypeCatalogId { get; set; }

    /// <summary>
    /// Estado del registro (Activo/Inactivo)
    /// </summary>
    [Required]
    [Column("EQP_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Navegación a la sucursal de gimnasio
    /// </summary>
    public GymBranch GymBranch { get; set; }

    /// <summary>
    /// Tipo de equipamiento
    /// </summary>
    public Catalog EquipmentTypeCatalog { get; set; }

    /// <summary>
    /// Navegación a las imágenes del equipamiento
    /// </summary>
    public ICollection<EquipmentImage> EquipmentImages { get; set; }
}

