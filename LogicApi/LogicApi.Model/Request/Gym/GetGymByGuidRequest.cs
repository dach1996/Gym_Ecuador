using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Gym;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Gym;

/// <summary>
/// Solicitud para obtener un gimnasio por GUID
/// </summary>
public class GetGymByGuidRequest : IRequest<GetGymByGuidResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    public Guid GymGuid { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    /// <param name="gymGuid"></param>
    public GetGymByGuidRequest(ContextRequest contextRequest, Guid gymGuid)
    {
        ContextRequest = contextRequest;
        GymGuid = gymGuid;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetGymByGuidRequest()
    {
    }
}
