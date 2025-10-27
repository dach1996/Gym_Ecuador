using LogicApi.Model.Response.GroupClass;

namespace LogicApi.Model.Request.GroupClass;

/// <summary>
/// Solicitud para crear una clase grupal
/// </summary>
public class CreateGroupClassRequest : IRequest<CreateGroupClassResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Guid del entrenador
    /// </summary>
    public Guid TrainerGuid { get; set; }

    /// <summary>
    /// Nombre de la clase
    /// </summary>
    public string ClassName { get; set; }

    /// <summary>
    /// Descripción de la clase
    /// </summary>
    public string ClassDescription { get; set; }

    /// <summary>
    /// Duración en minutos
    /// </summary>
    public int DurationMinutes { get; set; }

    /// <summary>
    /// Capacidad máxima
    /// </summary>
    public int MaxCapacity { get; set; }

    /// <summary>
    /// URL de imagen de la clase
    /// </summary>
    public string ClassImageUrl { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public CreateGroupClassRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateGroupClassRequest()
    {
    }
}
