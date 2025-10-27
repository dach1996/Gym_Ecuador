using LogicApi.Model.Response.Trainer;

namespace LogicApi.Model.Request.Trainer;

/// <summary>
/// Solicitud para actualizar un entrenador
/// </summary>
public class UpdateTrainerRequest : IRequest<UpdateTrainerResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del entrenador
    /// </summary>
    public Guid TrainerGuid { get; set; }

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
    /// Estado del entrenador
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public UpdateTrainerRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateTrainerRequest()
    {
    }
}
