using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Authentication;

/// <summary>
/// Function Table
/// </summary>
[Table(name: "FUNCION", Schema = "AUTENTICACION")]
public class Function
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("FUN_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Function Code
    /// </summary>
    [Required]
    [StringLength(64)]
    [Column("FUN_CODIGO")]
    public string Code { get; set; }

    /// <summary>
    /// Function Name
    /// </summary>
    [Required]
    [StringLength(64)]
    [Column("FUN_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// Function Description
    /// </summary>
    [Required]
    [StringLength(64)]
    [Column("FUN_DESCRIPCION")]
    public string Description { get; set; }

    /// <summary>
    /// Module Id
    /// </summary>
    [Required]
    [ForeignKey(nameof(Module))]
    [Column("MOD_ID")]
    public short ModuleId { get; set; }

    /// <summary>
    /// Function Status
    /// </summary>
    [Required]
    [Column("FUN_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Function Route
    /// </summary>
    [StringLength(64)]
    [Column("FUN_RUTA")]
    public string Route { get; set; }

    /// <summary>
    /// Function Icon
    /// </summary>
    [Required]
    [StringLength(64)]
    [Column("FUN_ICONO")]
    public string Icon { get; set; }

    /// <summary>
    /// Function Order
    /// </summary>
    [Required]
    [Column("FUN_ORDEN")]
    public byte Order { get; set; }

    /// <summary>
    /// Function Visibility
    /// </summary>
    [Required]
    [Column("FUN_VISUALIZAR")]
    public bool IsVisible { get; set; }

    /// <summary>
    /// Module
    /// </summary>
    public Module Module { get; set; }

    /// <summary>
    /// Function Functionalities List
    /// </summary>
    public ICollection<Functionality> Functionalities { get; set; }
}
