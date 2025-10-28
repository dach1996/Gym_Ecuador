using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicCommon.Model.Response;

namespace LogicApi.Model.Request.ProcessTracking;

/// <summary>
/// Solicitud para crear un seguimiento de proceso
/// </summary>
public class CreateProcessTrackingRequest : IApiBaseRequest<GenericCommonOperationResponse>
{
    /// <summary>
    /// Id de la persona
    /// </summary>
    [ValidateGuid]
    public Guid PersonGuid { get; set; }

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
    /// Comentarios u observaciones del entrenador o la persona.
    /// </summary>
    public string Observations { get; set; }

    /// <summary>
    /// Imágenes del seguimiento de proceso.
    /// </summary>
    public List<string> Base64Images { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

}
