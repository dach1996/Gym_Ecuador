using LogicApi.Model.Response.GymReview;

namespace LogicApi.Model.Request.GymReview;

/// <summary>
/// Solicitud para crear una reseña de gimnasio
/// </summary>
public class CreateGymReviewRequest : IRequest<CreateGymReviewResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Id de la persona
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// Calificación (1-5 estrellas)
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Comentario
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public CreateGymReviewRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateGymReviewRequest()
    {
    }
}
