using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.GymReview;

using Common.WebCommon.Models;
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
    public CommonContextRequest ContextRequest { get; set; }

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
