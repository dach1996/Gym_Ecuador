using LogicApi.Model.Response.Card;
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
    public ContextRequest ContextRequest { get; set; }
}