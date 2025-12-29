using LogicCommon.Model.Response;

using Common.WebCommon.Models;
using LogicCommon.Model.Request.File;
namespace LogicApi.Model.Request.ProcessTracking;

/// <summary>
/// Solicitud para crear un seguimiento de proceso
/// </summary>
public class CreateProcessTrackingRequest : IApiBaseRequest<GenericCommonOperationResponse>
{
    /// <summary>
    /// Peso corporal actual (en kg o la unidad estándar)
    /// </summary>
    [Required]
    [Range(0.01, 999.99)]
    public decimal Weight { get; set; }

    /// <summary>
    /// Altura de la persona (en cm) - No cambia con frecuencia, pero es clave para el IMC.
    /// </summary>
    [Required]
    [Range(0.01, 999.99)]
    public decimal Height { get; set; }

    /// <summary>
    /// Porcentaje de grasa corporal estimado.
    /// </summary>
    [Range(0.01, 99.99)]
    public decimal? BodyFatPercentage { get; set; }

    /// <summary>
    /// Porcentaje de masa muscular.
    /// </summary>
    [Range(0.01, 99.99)]
    public decimal? MuscleMassPercentage { get; set; }

    /// <summary>
    /// Circunferencia del pecho o tórax (cm).
    /// </summary>
    [Range(0.01, 99.99)]
    public decimal? ChestMeasurement { get; set; }

    /// <summary>
    /// Circunferencia de la cintura (cm). Clave para salud cardiovascular.
    /// </summary>
    [Range(0.01, 99.99)]
    public decimal? WaistMeasurement { get; set; }

    /// <summary>
    /// Circunferencia de la cadera (cm).
    /// </summary>
    [Range(0.01, 99.99)]
    public decimal? HipMeasurement { get; set; }

    /// <summary>
    /// Circunferencia del brazo derecho (cm).
    /// </summary>
    [Range(0.01, 99.99)]
    public decimal? ArmRightMeasurement { get; set; }

    /// <summary>
    /// Circunferencia del muslo derecho (cm).
    /// </summary>
    [Range(0.01, 99.99)]
    public decimal? ThighRightMeasurement { get; set; }

    /// <summary>
    /// Comentarios u observaciones del entrenador o la persona.
    /// </summary>
    [MaxLength(1024)]
    public string Observations { get; set; }

    /// <summary>
    /// Imágenes del seguimiento de proceso.
    /// </summary>
    public List<RequestEncodeFile> Images { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

}
