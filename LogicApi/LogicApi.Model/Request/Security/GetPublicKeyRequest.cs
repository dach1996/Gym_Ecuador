using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Security;
using MediatR;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Security;

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
