using Common.WebCommon.Attributes.CustomDataAnnotations;
using Common.WebCommon.Models;
using LogicCommon.Model.Request.File;
using LogicCommon.Model.Response;

namespace LogicApi.Model.Request.ProcessTracking;

/// <summary>
/// Solicitud para actualizar un seguimiento de proceso
/// </summary>
public class UpdateProcessTrackingRequest : IApiBaseRequest<GenericCommonOperationResponse>, IProcessTrackingMeasurementsInput
{
    /// <summary>
    /// Guid del seguimiento de proceso
    /// </summary>
    [ValidateGuid]
    public Guid ProcessTrackingGuid { get; set; }

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
