using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Authentication;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Entrenadores
/// </summary>
[Table(name: "ENTRENADORES", Schema = "CORE")]
public class Trainer
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ENT_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("ENT_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("ENT_FECHA_REGISTRO")]
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Id de la persona
    /// </summary>
    [Required]
    [Column("PNA_ID")]
    public int PersonId { get; set; }

    /// <summary>
    /// Biografía del entrenador
    /// </summary>
    [StringLength(2000)]
    [Column("ENT_BIOGRAFIA")]
    public string Biography { get; set; }

    /// <summary>
    /// Estado del entrenador
    /// </summary>
    [Required]
    [Column("ENT_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Id de usuario registrador
    /// </summary>
    [Column("USR_ID_REGISTRADOR")]
    public int? UserIdRegister { get; set; }

    /// <summary>
    /// Navegación a la persona
    /// </summary>
    [ForeignKey(nameof(PersonId))]
    public Person Person { get; set; }

    /// <summary>
    /// Navegación a los gimnasios del entrenador
    /// </summary>
    public ICollection<TrainerGym> TrainerGyms { get; set; }
}
