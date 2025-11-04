using LogicApi.Model.Response.Gym;

namespace LogicApi.Model.Request.Gym;

/// <summary>
/// Solicitud para crear un gimnasio
/// </summary>
public class CreateGymRequest : IRequest<CreateGymResponse>, IApiBaseRequest
{
    /// <summary>
    /// Nombre del gimnasio
    /// </summary>
    [Required]	
    public string Name { get; set; }

    /// <summary>
    /// Descripción del gimnasio
    /// </summary>  
    [Required]
    public string Description { get; set; }

    /// <summary>
    /// Descripción corta del gimnasio
    /// </summary>
    public string ShortDescription { get; set; }

    /// <summary>
    /// Dirección del gimnasio
    /// </summary>
    [Required]
    public string Address { get; set; }

    /// <summary>
    /// Teléfono del gimnasio
    /// </summary>
    [Required]
    public string Phone { get; set; }

    /// <summary>
    /// Email del gimnasio
    /// </summary>  
    [Required]
    public string Email { get; set; }

    /// <summary>
    /// Sitio web del gimnasio
    /// </summary>
    public string Website { get; set; }

    /// <summary>
    /// Latitud para localización
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// Longitud para localización
    /// </summary>
    public decimal Longitude { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public CreateGymRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateGymRequest()
    {
    }
}
