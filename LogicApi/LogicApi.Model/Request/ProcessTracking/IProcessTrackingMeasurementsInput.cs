namespace LogicApi.Model.Request.ProcessTracking;

/// <summary>
/// Contrato de campos planos de medidas en requests de seguimiento de proceso
/// </summary>
public interface IProcessTrackingMeasurementsInput
{
    /// <summary>
    /// Peso corporal actual (en kg o la unidad estándar)
    /// </summary>
    decimal Weight { get; }
    /// <summary>
    /// Altura de la persona (en cm) - No cambia con frecuencia, pero es clave para el IMC.
    /// </summary>
    decimal Height { get; }
    /// <summary>
    /// Porcentaje de grasa corporal estimado.
    /// </summary>
    decimal? BodyFatPercentage { get; }
    /// <summary>
    /// Porcentaje de masa muscular.
    /// </summary>
    decimal? MuscleMassPercentage { get; }
    /// <summary>
    /// Circunferencia del pecho o tórax (cm).
    /// </summary>
    decimal? ChestMeasurement { get; }
    /// <summary>
    /// Circunferencia de la cintura (cm). Clave para salud cardiovascular.
    /// </summary>
    decimal? WaistMeasurement { get; }
    /// <summary>
    /// Circunferencia de la cadera (cm).
    /// </summary>
    decimal? HipMeasurement { get; }
    /// <summary>
    /// Circunferencia del brazo derecho (cm).
    /// </summary>
    decimal? ArmRightMeasurement { get; }
    /// <summary>
    /// Circunferencia del muslo derecho (cm).
    /// </summary>
    decimal? ThighRightMeasurement { get; }
}
