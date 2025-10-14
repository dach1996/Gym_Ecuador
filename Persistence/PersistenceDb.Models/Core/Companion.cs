using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PersistenceDb.Models.Authentication;
namespace PersistenceDb.Models.Core;
/// <summary>
/// Tabla Acompañante
/// </summary>
[Table(name: "ACOMPAÑANTE", Schema = "CORE")]
[PrimaryKey(nameof(UserId), nameof(PersonId))]
public class Companion
{

    /// <summary>
    /// Id Usuario
    /// </summary>
    /// <value></value>
    [ForeignKey(nameof(User))]
    [Column("USR_ID")]
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// Id Persona
    /// </summary>
    /// <value></value>
    [ForeignKey(nameof(Person))]
    [Column("PNA_ID")]
    [Required]
    public int PersonId { get; set; }

    /// <summary>
    /// Fecha de Registro
    /// </summary>
    /// <value></value>
    [Column("ACM_FECHA_REGISTRO")]
    [Required]
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Es favorito
    /// </summary>
    /// <value></value>
    [Column("ACM_FAVORITO")]
    [Required]
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Persona
    /// </summary>
    /// <value></value>
    public Person Person { get; set; }

    /// <summary>
    /// Usuario
    /// </summary>
    /// <value></value>
    public User User { get; set; }
}
