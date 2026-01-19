using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Authentication;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Cliente Sucursal Gimnasio
/// Relaciona usuarios con sucursales de gimnasio
/// </summary>
[Table(name: "CLIENTE_SUCURSAL_GIMNASIO", Schema = "CORE")]
public class ClientGymBranch
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CSG_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("CSG_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id del usuario
    /// </summary>
    [Required]
    [Column("PNA_ID")]
    [ForeignKey(nameof(Person))]
    public int PersonId { get; set; }

    /// <summary>
    /// Id de la sucursal de gimnasio
    /// </summary>
    [Required]
    [Column("SGY_ID")]
    [ForeignKey(nameof(GymBranch))]
    public int GymBranchId { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Required]
    [Column("CSG_FECHA_REGISTRO")]
    public DateTime RegistrationDate { get; set; }

    /// <summary>
    /// Estado del registro
    /// </summary>
    [Required]
    [Column("CSG_ESTADO")]
    public bool Status { get; set; }

    /// <summary>
    /// Navegación al usuario
    /// </summary>
    public Person Person { get; set; }

    /// <summary>
    /// Navegación a la sucursal de gimnasio
    /// </summary>
    public GymBranch GymBranch { get; set; }

    /// <summary>
    /// Navegación a las membresías del cliente
    /// </summary>
    public ICollection<ClientMembership> ClientMemberships { get; set; }
}

