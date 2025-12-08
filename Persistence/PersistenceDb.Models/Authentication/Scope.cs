using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Authentication;

/// <summary>
/// Scope Table
/// </summary>
[Table(name: "AMBITO", Schema = "AUTENTICACION")]
public class Scope
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("AMB_ID")]
    public byte Id { get; set; }

    /// <summary>
    /// Scope Code
    /// </summary>
    [Required]
    [StringLength(64)]
    [Column("AMB_CODIGO")]
    public string Code { get; set; }

    /// <summary>
    /// Scope Name
    /// </summary>
    [Required]
    [StringLength(64)]
    [Column("AMB_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// User Role Scope List
    /// </summary>
    public ICollection<UserRoleScope> UserRoleScopes { get; set; }
}
