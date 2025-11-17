using LogicApi.Model.Response.Service;

namespace LogicApi.Model.Request.Service;

/// <summary>
/// Solicitud para obtener un servicio por ID
/// </summary>
public class GetServiceByIdRequest : IRequest<GetServiceByIdResponse>, IApiBaseRequest
{
    /// <summary>
    /// Id del servicio
    /// </summary>
    [Required]
    public int ServiceId { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public GetServiceByIdRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetServiceByIdRequest()
    {
    }
}

