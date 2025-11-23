using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.ProcessTracking;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.ProcessTracking;

/// <summary>
/// Solicitud para actualizar un seguimiento de proceso
/// </summary>
public class UpdateProcessTrackingRequest : IRequest<UpdateProcessTrackingResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del seguimiento de proceso
    /// </summary>
    public Guid ProcessTrackingGuid { get; set; }

    /// <summary>
    /// Tipo de proceso
    /// </summary>
    public string ProcessType { get; set; }

    /// <summary>
    /// Nombre del proceso
    /// </summary>
    public string ProcessName { get; set; }

    /// <summary>
    /// Descripción del proceso
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Estado del proceso
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Fecha de fin
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Progreso (porcentaje)
    /// </summary>
    public decimal? Progress { get; set; }

    /// <summary>
    /// Notas adicionales
    /// </summary>
    public string Notes { get; set; }

    /// <summary>
    /// Estado activo
    /// </summary>
    public bool IsActive { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public UpdateProcessTrackingRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateProcessTrackingRequest()
    {
    }
}
