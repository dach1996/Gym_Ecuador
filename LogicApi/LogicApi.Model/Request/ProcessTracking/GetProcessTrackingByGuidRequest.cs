using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.ProcessTracking;

using Common.WebCommon.Models;
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
    public CommonContextRequest ContextRequest { get; set; }

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
