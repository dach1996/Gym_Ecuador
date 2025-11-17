using LogicApi.Model.Response.Service;

namespace LogicApi.Model.Request.Service;

/// <summary>
/// Solicitud para actualizar un servicio
/// </summary>
public class UpdateServiceRequest : IRequest<UpdateServiceResponse>, IApiBaseRequest
{
    /// <summary>
    /// Id del servicio
    /// </summary>
    [Required]
    public int ServiceId { get; set; }

    /// <summary>
    /// Nombre del servicio
    /// </summary>
    [StringLength(200)]
    public string Name { get; set; }

    /// <summary>
    /// Descripción del servicio
    /// </summary>
    [StringLength(1000)]
    public string Description { get; set; }

    /// <summary>
    /// Indica si el servicio requiere reserva previa
    /// </summary>
    public bool? RequiresReservation { get; set; }

    /// <summary>
    /// Estado del servicio
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public UpdateServiceRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateServiceRequest()
    {
    }
}

