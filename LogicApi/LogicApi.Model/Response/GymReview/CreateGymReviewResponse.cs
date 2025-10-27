namespace LogicApi.Model.Response.GymReview;

/// <summary>
/// Respuesta de crear reseña de gimnasio
/// </summary>
public class CreateGymReviewResponse : IApiBaseResponse
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
    /// Guid de la reseña creada
    /// </summary>
    public Guid GymReviewGuid { get; set; }

    /// <summary>
    /// Calificación
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Fecha de reseña
    /// </summary>
    public DateTime ReviewDate { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="gymReviewGuid"></param>
    /// <param name="rating"></param>
    /// <param name="reviewDate"></param>
    public CreateGymReviewResponse(Guid gymReviewGuid, int rating, DateTime reviewDate)
    {
        GymReviewGuid = gymReviewGuid;
        Rating = rating;
        ReviewDate = reviewDate;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateGymReviewResponse()
    {
    }
}
