using Common.WebApi.Models.ContextRequestModel;
using LogicAdministratorApi.Model.Response.Security;
using Common.WebCommon.Models;
namespace LogicAdministratorApi.Model.Request.Security;

/// <summary>
/// Modelo Request para obtener llave pública
/// </summary>
public class GetPublicKeyRequest : IRequest<GetPublicKeyResponse>, IApiBaseRequest
{
    /// <summary>
    /// Identificador, puede ser tipo : General, Bank
    /// </summary>
    /// <value></value>
    [Required]
    public string RsaImplementation { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

