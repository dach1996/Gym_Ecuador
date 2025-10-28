using LogicApi.Model.Response.ProcessTracking;

namespace LogicApi.Model.Request.ProcessTracking;

/// <summary>
/// Solicitud para obtener un seguimiento de proceso por GUID
/// </summary>
public class GetProcessTrackingByGuidRequest : IRequest<GetProcessTrackingByGuidResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del seguimiento de proceso
    /// </summary>
    public Guid ProcessTrackingGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public GetProcessTrackingByGuidRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetProcessTrackingByGuidRequest()
    {
    }
}
