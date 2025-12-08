using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Authentication;

/// <summary>
/// Role Functionality Table
/// </summary>
[Table(name: "ROL_FUNCIONALIDAD", Schema = "AUTENTICACION")]
public class RoleFunctionality
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("RLF_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Role Id
    /// </summary>
    [Required]
    [ForeignKey(nameof(Role))]
    [Column("ROL_ID")]
    public int RoleId { get; set; }

    /// <summary>
    /// Functionality Id
    /// </summary>
    [Required]
    [ForeignKey(nameof(Functionality))]
    [Column("FUC_ID")]
    public Guid FunctionalityId { get; set; }

    /// <summary>
    /// Role
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    /// Functionality
    /// </summary>
    public Functionality Functionality { get; set; }
}
