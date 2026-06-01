using LogicCommon.Model.Response;
using Common.WebCommon.Models;
using LogicCommon.Model.Request.File;

namespace LogicApi.Model.Request.ProcessTracking;

/// <summary>
/// Solicitud para crear un seguimiento de proceso
/// </summary>
public class CreateProcessTrackingRequest : IApiBaseRequest<GenericCommonOperationResponse>, IProcessTrackingMeasurementsInput
{
    /// <summary>
    /// Listado de medidas (código + valor)
    /// </summary>
    [Required]
    public List<ProcessTrackingMeasurementInput> Measurements { get; set; }

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
