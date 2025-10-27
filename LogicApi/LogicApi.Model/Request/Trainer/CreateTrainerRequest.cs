using LogicApi.Model.Response.Trainer;

namespace LogicApi.Model.Request.Trainer;

/// <summary>
/// Solicitud para crear un entrenador
/// </summary>
public class CreateTrainerRequest : IRequest<CreateTrainerResponse>, IApiBaseRequest
{
    /// <summary>
    /// Id de la persona
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Especialidad del entrenador
    /// </summary>
    public string Specialty { get; set; }

    /// <summary>
    /// Biografía del entrenador
    /// </summary>
    public string Biography { get; set; }

    /// <summary>
    /// URL de foto de perfil
    /// </summary>
    public string ProfilePhotoUrl { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public CreateTrainerRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateTrainerRequest()
    {
    }
}
