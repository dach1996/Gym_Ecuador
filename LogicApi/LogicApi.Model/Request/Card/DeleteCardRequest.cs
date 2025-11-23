using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response;
using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Card;
/// <summary>
/// Request eliminar tarjeta
/// </summary>
public class DeleteCardRequest : IApiBaseRequest<HandlerResponse>
{
    /// <summary>
    /// Id de Tarjeta
    /// </summary>
    /// <value></value>
    [Required]
    [Range(1, int.MaxValue)]
    public int CardId { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}