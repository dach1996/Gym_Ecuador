using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Authentication;

/// <summary>
/// Functionality Table
/// </summary>
[Table(name: "FUNCIONALIDAD", Schema = "AUTENTICACION")]
public class Functionality
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [Column("FUC_ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// Functionality Code
    /// </summary>
    [Required]
    [StringLength(64)]
    [Column("FUC_CODIGO")]
    public string Code { get; set; }

    /// <summary>
    /// Functionality Name
    /// </summary>
    [Required]
    [StringLength(64)]
    [Column("FUC_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// Functionality Description
    /// </summary>
    [Required]
    [StringLength(64)]
    [Column("FUC_DESCRIPCION")]
    public string Description { get; set; }

    /// <summary>
    /// Function Id
    /// </summary>
    [Required]
    [ForeignKey(nameof(Function))]
    [Column("FUN_ID")]
    public int FunctionId { get; set; }

    /// <summary>
    /// Functionality Status
    /// </summary>
    [Required]
    [Column("FUC_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Function
    /// </summary>
    public Function Function { get; set; }

    /// <summary>
    /// Role Functionalities List
    /// </summary>
    public ICollection<RoleFunctionality> RoleFunctionalities { get; set; }
}
