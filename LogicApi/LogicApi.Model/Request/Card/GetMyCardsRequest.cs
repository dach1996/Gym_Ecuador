using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Card;
using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Card;
/// <summary>
/// Request de obtener mis tarjetas
/// </summary>
public class GetMyCardsRequest : IApiBaseRequest<GetMyCardsResponse>
{
 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}