using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Authentication;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Objetivos Personales
/// </summary>
[Table(name: "OBJETIVOS_PERSONALES", Schema = "CORE")]
public class PersonalGoal
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("OBP_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("OBP_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id de la persona
    /// </summary>
    [Required]
    [Column("PNA_ID")]
    public int PersonId { get; set; }

    /// <summary>
    /// Tipo de objetivo
    /// </summary>
    [Required]
    [StringLength(100)]
    [Column("OBP_TIPO_OBJETIVO")]
    public string GoalType { get; set; } // Perder peso, Ganar músculo, Aumentar fuerza

    /// <summary>
    /// Valor inicial
    /// </summary>
    [Column("OBP_VALOR_INICIAL")]
    public decimal? InitialValue { get; set; }

    /// <summary>
    /// Valor objetivo
    /// </summary>
    [Required]
    [Column("OBP_VALOR_OBJETIVO")]
    public decimal TargetValue { get; set; }

    /// <summary>
    /// Fecha de inicio del objetivo
    /// </summary>
    [Required]
    [Column("OBP_FECHA_INICIO_OBJETIVO")]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de fin estimada
    /// </summary>
    [Column("OBP_FECHA_FIN_ESTIMADA")]
    public DateTime? EstimatedEndDate { get; set; }

    /// <summary>
    /// Estado del objetivo
    /// </summary>
    [Required]
    [StringLength(50)]
    [Column("OBP_ESTADO_OBJETIVO")]
    public string GoalStatus { get; set; } // Activo, Completado, Abandonado

    /// <summary>
    /// Descripción del objetivo
    /// </summary>
    [StringLength(1000)]
    [Column("OBP_DESCRIPCION")]
    public string Description { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("OBP_FECHA_REGISTRO")]
    public DateTime? DateTimeRegister { get; set; }

    /// <summary>
    /// Id de usuario registrador
    /// </summary>
    [Column("USR_ID_REGISTRADOR")]
    public int? UserIdRegister { get; set; }

    /// <summary>
    /// Navegación a la persona
    /// </summary>
    [ForeignKey("PersonId")]
    public virtual Person Person { get; set; }
}
