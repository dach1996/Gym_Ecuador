using Common.WebApi.Models.ContextRequestModel;
using Common.WebCommon.Attributes;
using LogicApi.Model.Response.Card;
using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Card;
/// <summary>
/// Request validar tarjeta
/// </summary>
public class ValidateCardRequest : IApiBaseRequest<ValidateCardResponse>
{
    /// <summary>
    /// Número de Tarjeta
    /// </summary>
    /// <value></value>
    [Required]
    [EncryptedField]
    [IgnoreSensible]
    public string CardNumber { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}