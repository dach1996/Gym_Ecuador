using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Service;

using Common.WebCommon.Models;
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
    public CommonContextRequest ContextRequest { get; set; }

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

