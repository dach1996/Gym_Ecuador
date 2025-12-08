using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Authentication;

/// <summary>
/// Module Table
/// </summary>
[Table(name: "MODULO", Schema = "AUTENTICACION")]
public class Module
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("MOD_ID")]
    public short Id { get; set; }

    /// <summary>
    /// Module Code
    /// </summary>
    [Required]
    [StringLength(32)]
    [Column("MOD_CODIGO")]
    public string Code { get; set; }

    /// <summary>
    /// Module Name
    /// </summary>
    [Required]
    [StringLength(32)]
    [Column("MOD_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// Module Description
    /// </summary>
    [Required]
    [StringLength(64)]
    [Column("MOD_DESCRIPCION")]
    public string Description { get; set; }

    /// <summary>
    /// Module Status
    /// </summary>
    [Required]
    [Column("MOD_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Module Icon
    /// </summary>
    [Required]
    [StringLength(64)]
    [Column("MOD_ICONO")]
    public string Icon { get; set; }

    /// <summary>
    /// Module Order
    /// </summary>
    [Required]
    [Column("MOD_ORDEN")]
    public byte Order { get; set; }

    /// <summary>
    /// Module Functions List
    /// </summary>
    public ICollection<Function> Functions { get; set; }
}
