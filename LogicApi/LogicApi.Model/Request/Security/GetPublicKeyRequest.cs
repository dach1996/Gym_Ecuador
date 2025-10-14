using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Common.WebApi.Models;
using LogicApi.Model.Response.Security;
using MediatR;

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
    public ContextRequest ContextRequest { get; set; }
}
