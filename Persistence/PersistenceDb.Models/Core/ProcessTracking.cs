using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Authentication;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Seguimiento de Procesos
/// </summary>
[Table(name: "SEGUIMIENTO_PROCESOS", Schema = "CORE")]
public class ProcessTracking
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("SPR_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("SPR_FECHA_REGISTRO")]
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Id de la persona
    /// </summary>
    [Required]
    [Column("PNA_ID")]
    public int PersonId { get; set; }

    /// <summary>
    /// Id del gimnasio
    /// </summary>
    [Required]
    [Column("GYM_ID")]
    public int? GymId { get; set; }

    /// <summary>
    /// Peso corporal actual (en kg o la unidad estándar)
    /// </summary>
    [Column("SPR_PESO")]
    public decimal Weight { get; set; }

    /// <summary>
    /// Altura de la persona (en cm) - No cambia con frecuencia, pero es clave para el IMC.
    /// </summary>
    [Column("SPR_ALTURA")]
    public decimal Height { get; set; }

    /// <summary>
    /// Porcentaje de grasa corporal estimado.
    /// </summary>
    [Column("SPR_GRASA_PORCENTAJE")]
    public decimal? BodyFatPercentage { get; set; }

    /// <summary>
    /// Porcentaje de masa muscular.
    /// </summary>
    [Column("SPR_MUSCULO_PORCENTAJE")]
    public decimal? MuscleMassPercentage { get; set; }

    /// <summary>
    /// Circunferencia del pecho o tórax (cm).
    /// </summary>
    [Column("SPR_MEDIDA_PECHO")]
    public decimal? ChestMeasurement { get; set; }

    /// <summary>
    /// Circunferencia de la cintura (cm). Clave para salud cardiovascular.
    /// </summary>
    [Column("SPR_MEDIDA_CINTURA")]
    public decimal? WaistMeasurement { get; set; }

    /// <summary>
    /// Circunferencia de la cadera (cm).
    /// </summary>
    [Column("SPR_MEDIDA_CADERA")]
    public decimal? HipMeasurement { get; set; }

    /// <summary>
    /// Circunferencia del brazo derecho (cm).
    /// </summary>
    [Column("SPR_MEDIDA_BRAZO_DER")]
    public decimal? ArmRightMeasurement { get; set; }

    /// <summary>
    /// Circunferencia del muslo derecho (cm).
    /// </summary>
    [Column("SPR_MEDIDA_MUSLO_DER")]
    public decimal? ThighRightMeasurement { get; set; }

    /// <summary>
    /// Comentarios u observaciones del entrenador o la persona.
    /// </summary>
    [Column("SPR_OBSERVACIONES")]
    public string Observations { get; set; }

    /// <summary>
    /// Id de usuario registrador
    /// </summary>
    [Column("USR_ID_REGISTRADOR")]
    public int UserIdRegister { get; set; }

    /// <summary>
    /// Navegación a la persona
    /// </summary>
    [ForeignKey(nameof(PersonId))]
    public Person Person { get; set; }

    /// <summary>
    /// Navegación al gimnasio
    /// </summary>
    [ForeignKey(nameof(GymId))]
    public Gym Gym { get; set; }

    /// <summary>
    /// Colección de imágenes del seguimiento
    /// </summary>
    public virtual ICollection<ProcessTrackingImage> ProcessTrackingImages { get; set; }
}
