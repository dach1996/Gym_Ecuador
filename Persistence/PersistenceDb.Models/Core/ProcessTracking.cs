using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
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
    /// Guid
    /// </summary>
    [Required]
    [Column("SPR_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Peso corporal actual (en kg o la unidad estándar)
    /// </summary>
    [Column("SPR_PESO")]
    [Precision(5, 2)]
    public decimal Weight { get; set; }

    /// <summary>
    /// Altura de la persona (en cm) - No cambia con frecuencia, pero es clave para el IMC.
    /// </summary>
    [Column("SPR_ALTURA")]
    [Precision(5, 2)]
    public decimal Height { get; set; }

    /// <summary>
    /// Porcentaje de grasa corporal estimado.
    /// </summary>
    [Column("SPR_GRASA_PORCENTAJE")]
    [Precision(4, 2)]
    public decimal? BodyFatPercentage { get; set; }

    /// <summary>
    /// Porcentaje de masa muscular.
    /// </summary>
    [Column("SPR_MUSCULO_PORCENTAJE")]
    [Precision(4, 2)]
    public decimal? MuscleMassPercentage { get; set; }

    /// <summary>
    /// Circunferencia del pecho o tórax (cm).
    /// </summary>
    [Column("SPR_MEDIDA_PECHO")]
    [Precision(5, 2)]
    public decimal? ChestMeasurement { get; set; }

    /// <summary>
    /// Circunferencia de la cintura (cm). Clave para salud cardiovascular.
    /// </summary>
    [Column("SPR_MEDIDA_CINTURA")]
    [Precision(5, 2)]
    public decimal? WaistMeasurement { get; set; }

    /// <summary>
    /// Circunferencia de la cadera (cm).
    /// </summary>
    [Column("SPR_MEDIDA_CADERA")]
    [Precision(5, 2)]
    public decimal? HipMeasurement { get; set; }

    /// <summary>
    /// Circunferencia del brazo derecho (cm).
    /// </summary>
    [Column("SPR_MEDIDA_BRAZO_DER")]
    [Precision(5, 2)]
    public decimal? ArmRightMeasurement { get; set; }

    /// <summary>
    /// Circunferencia del muslo derecho (cm).
    /// </summary>
    [Column("SPR_MEDIDA_MUSLO_DER")]
    [Precision(5, 2)]
    public decimal? ThighRightMeasurement { get; set; }

    /// <summary>
    /// Comentarios u observaciones del entrenador o la persona.
    /// </summary>
    [Column("SPR_OBSERVACIONES")]
    public string Observations { get; set; }

    /// <summary>
    /// Id de usuario registrado por el entrenador
    /// </summary>
    [Column("EGY_ID")]
    public int? GymTrainerId { get; set; }

    /// <summary>
    /// Id de usuario que realizó la última actualización
    /// </summary>
    [Column("USR_ID")]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    /// <summary>
    /// Navegación al usuario registrador
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Colección de imágenes del seguimiento
    /// </summary>
    public virtual ICollection<ProcessTrackingImage> ProcessTrackingImages { get; set; }
}
