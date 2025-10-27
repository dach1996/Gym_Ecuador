using LogicApi.Model.Response.Trainer;

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
    public ContextRequest ContextRequest { get; set; }

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
