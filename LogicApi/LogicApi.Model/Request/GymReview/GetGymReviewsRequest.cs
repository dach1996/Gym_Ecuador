using LogicApi.Model.Response.GymReview;

namespace LogicApi.Model.Request.GymReview;

/// <summary>
/// Solicitud para obtener reseñas de gimnasio
/// </summary>
public class GetGymReviewsRequest : IRequest<GetGymReviewsResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Filtro por calificación mínima
    /// </summary>
    public int? MinRating { get; set; }

    /// <summary>
    /// Página
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Tamaño de página
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public GetGymReviewsRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetGymReviewsRequest()
    {
    }
}
