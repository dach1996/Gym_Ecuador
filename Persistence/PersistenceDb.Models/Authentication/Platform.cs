using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Authentication;

/// <summary>
/// Platform Table
/// </summary>
[Table(name: "PLATAFORMA", Schema = "AUTENTICACION")]
public class Platform
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("PTF_ID")]
    public byte Id { get; set; }

    /// <summary>
    /// Platform Code
    /// </summary>
    [Required]
    [StringLength(32)]
    [Column("PTF_CODIGO")]
    public string Code { get; set; }

    /// <summary>
    /// Platform Name
    /// </summary>
    [Required]
    [StringLength(32)]
    [Column("PTF_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// Platform Roles List
    /// </summary>
    public ICollection<Role> Roles { get; set; }
}
