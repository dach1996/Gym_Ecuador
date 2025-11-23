using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Service;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Service;

/// <summary>
/// Solicitud para crear un servicio
/// </summary>
public class CreateServiceRequest : IRequest<CreateServiceResponse>, IApiBaseRequest
{
    /// <summary>
    /// Nombre del servicio
    /// </summary>
    [Required]
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
    public bool RequiresReservation { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public CreateServiceRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateServiceRequest()
    {
    }
}

