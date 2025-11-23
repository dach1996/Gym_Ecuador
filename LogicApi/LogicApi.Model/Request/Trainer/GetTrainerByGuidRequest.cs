using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Trainer;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Trainer;

/// <summary>
/// Solicitud para obtener un entrenador por GUID
/// </summary>
public class GetTrainerByGuidRequest : IRequest<GetTrainerByGuidResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del entrenador
    /// </summary>
    public Guid TrainerGuid { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    /// <param name="trainerGuid"></param>
    public GetTrainerByGuidRequest(ContextRequest contextRequest, Guid trainerGuid)
    {
        ContextRequest = contextRequest;
        TrainerGuid = trainerGuid;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetTrainerByGuidRequest()
    {
    }
}
