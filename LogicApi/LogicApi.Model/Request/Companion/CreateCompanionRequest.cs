using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response;
using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Companion;
/// <summary>
/// Request para crear compañero de viaje
/// </summary>
public class CreateOrUpdateCompanionRequest : IApiBaseRequest<HandlerResponse>
{
    /// <summary>
    /// Id de Personas
    /// </summary>
    /// <value></value>
    [Required]
    public IEnumerable<long> PersonIds { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public CreateOrUpdateCompanionRequest()
    {

    }

    /// <summary>
    /// Constructor 
    /// </summary>
    /// <param name="personId"></param>
    /// <param name="contextRequest"></param>
    public CreateOrUpdateCompanionRequest(long personId, ContextRequest contextRequest)
    {
        PersonIds = new[] { personId };
        ContextRequest = contextRequest;
    }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}