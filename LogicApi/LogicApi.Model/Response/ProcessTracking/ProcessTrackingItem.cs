using LogicApi.Model.Response.Common.ProcessTracking;
using LogicCommon.Model.Response.File;

namespace LogicApi.Model.Response.ProcessTracking;

/// <summary>
/// Item de seguimiento de proceso
/// </summary>
public class ProcessTrackingItem : IProcessTrackingMeasurement
{
    /// <summary>
    /// Guid del seguimiento de proceso
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime RegistrationDate { get; set; }

    /// <summary>
    /// Peso corporal actual (en kg o la unidad estándar)
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Altura de la persona (en cm) - No cambia con frecuencia, pero es clave para el IMC.
    /// </summary>
    public decimal Height { get; set; }

    /// <summary>
    /// Porcentaje de grasa corporal estimado.
    /// </summary>
    public decimal? BodyFatPercentage { get; set; }

    /// <summary>
    /// Porcentaje de masa muscular.
    /// </summary>
    public decimal? MuscleMassPercentage { get; set; }

    /// <summary>
    /// Circunferencia del pecho o tórax (cm).
    /// </summary>
    public decimal? ChestMeasurement { get; set; }

    /// <summary>
    /// Circunferencia de la cintura (cm). Clave para salud cardiovascular.
    /// </summary>
    public decimal? WaistMeasurement { get; set; }

    /// <summary>
    /// Circunferencia de la cadera (cm).
    /// </summary>
    public decimal? HipMeasurement { get; set; }

    /// <summary>
    /// Circunferencia del brazo derecho (cm).
    /// </summary>
    public decimal? ArmRightMeasurement { get; set; }

    /// <summary>
    /// Circunferencia del muslo derecho (cm).
    /// </summary>
    public decimal? ThighRightMeasurement { get; set; }

    /// <summary>
    /// Lista de imágenes asociadas al seguimiento de proceso.
    /// </summary>
    public List<FileUrlResponse> Images { get; set; } = [];
}
