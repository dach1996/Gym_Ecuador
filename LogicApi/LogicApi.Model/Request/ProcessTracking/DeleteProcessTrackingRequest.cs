using LogicCommon.Model.Response;
using Common.WebCommon.Models;

namespace LogicApi.Model.Request.ProcessTracking;

/// <summary>
/// Solicitud para eliminar un seguimiento de proceso
/// </summary>
public class DeleteProcessTrackingRequest : IApiBaseRequest<GenericCommonOperationResponse>
{
    /// <summary>
    /// Guid del seguimiento de proceso
    /// </summary>
    public Guid ProcessTrackingGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

