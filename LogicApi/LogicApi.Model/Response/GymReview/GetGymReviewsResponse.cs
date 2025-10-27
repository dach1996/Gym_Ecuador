namespace LogicApi.Model.Response.GymReview;

/// <summary>
/// Respuesta de obtener reseñas de gimnasio
/// </summary>
public class GetGymReviewsResponse : IApiBaseResponse
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    public bool ShowMessage { get; set; }

    /// <summary>
    /// Lista de reseñas
    /// </summary>
    public IEnumerable<GymReviewItem> GymReviews { get; set; }

    /// <summary>
    /// Calificación promedio
    /// </summary>
    public decimal AverageRating { get; set; }

    /// <summary>
    /// Total de reseñas
    /// </summary>
    public int TotalReviews { get; set; }

    /// <summary>
    /// Total de registros
    /// </summary>
    public int TotalRecords { get; set; }

    /// <summary>
    /// Página actual
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Total de páginas
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="gymReviews"></param>
    /// <param name="averageRating"></param>
    /// <param name="totalReviews"></param>
    /// <param name="totalRecords"></param>
    /// <param name="currentPage"></param>
    /// <param name="pageSize"></param>
    public GetGymReviewsResponse(IEnumerable<GymReviewItem> gymReviews, decimal averageRating, int totalReviews, int totalRecords, int currentPage, int pageSize)
    {
        GymReviews = gymReviews;
        AverageRating = averageRating;
        TotalReviews = totalReviews;
        TotalRecords = totalRecords;
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetGymReviewsResponse()
    {
    }
}

/// <summary>
/// Item de reseña de gimnasio
/// </summary>
public class GymReviewItem
{
    /// <summary>
    /// Guid de la reseña
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre completo de la persona
    /// </summary>
    public string PersonFullName { get; set; }

    /// <summary>
    /// Calificación (1-5 estrellas)
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Comentario
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// Fecha de reseña
    /// </summary>
    public DateTime ReviewDate { get; set; }

    /// <summary>
    /// Estado de la reseña
    /// </summary>
    public bool IsActive { get; set; }
}
