using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Authentication;

/// <summary>
/// User Role Scope Table
/// </summary>
[Table(name: "USUARIO_ROL_AMBITO", Schema = "AUTENTICACION")]
public class UserRoleScope
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("URA_ID")]
    public int Id { get; set; }

    /// <summary>
    /// User Id
    /// </summary>
    [Required]
    [ForeignKey(nameof(User))]
    [Column("USR_ID")]
    public int UserId { get; set; }

    /// <summary>
    /// Role Id
    /// </summary>
    [Required]
    [ForeignKey(nameof(Role))]
    [Column("ROL_ID")]
    public int RoleId { get; set; }

    /// <summary>
    /// Scope Identifier
    /// </summary>
    [Column("URA_IDENTIFICADOR")]
    public int? ScopeIdentifier { get; set; }

    /// <summary>
    /// User
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Role
    /// </summary>
    public Role Role { get; set; }

}
