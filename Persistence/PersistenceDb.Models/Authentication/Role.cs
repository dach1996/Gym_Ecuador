using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Enums;

namespace PersistenceDb.Models.Authentication;

/// <summary>
/// Role Table
/// </summary>
[Table(name: "ROL", Schema = "AUTENTICACION")]
public class Role
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ROL_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Role Guid
    /// </summary>
    [Required]
    [Column("ROL_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Role Name
    /// </summary>
    [Required]
    [StringLength(32)]
    [Column("ROL_CODIGO")]
    public string Code { get; set; }

    /// <summary>
    /// Role Description
    /// </summary>
    [Required]
    [StringLength(64)]
    [Column("ROL_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// Platform Id
    /// </summary>
    [Required]
    [ForeignKey(nameof(Platform))]
    [Column("PTF_ID")]
    public byte PlatformId { get; set; }

    /// <summary>
    /// Alcance del rol
    /// </summary>
    [Required]
    [Column("ROL_ALCANCE")]
    public RoleScope Scope { get; set; }

    /// <summary>
    /// Platform
    /// </summary>
    public Platform Platform { get; set; }

    /// <summary>
    /// Role Functionalities List
    /// </summary>
    public ICollection<RoleFunctionality> RoleFunctionalities { get; set; }

    /// <summary>
    /// User Role Scopes List
    /// </summary>
    public ICollection<UserRoleScope> UserRoleScopes { get; set; }
}
