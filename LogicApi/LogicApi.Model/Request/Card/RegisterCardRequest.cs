using Common.WebCommon.Attributes;
using LogicApi.Model.Response;
namespace LogicApi.Model.Request.Card;
/// <summary>
/// Request para registrar tarjeta
/// </summary>
public class RegisterCardRequest : IApiBaseRequest<HandlerResponse>
{
    /// <summary>
    /// Nombre
    /// </summary>
    /// <value></value>
    [EncryptedField]
    [Required]
    public string CardOwner { get; set; }

    /// <summary>
    /// Número de Tarjeta
    /// </summary>
    /// <value></value>
    [EncryptedField]
    [IgnoreSensible]
    [Required]
    public string CardNumber { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}