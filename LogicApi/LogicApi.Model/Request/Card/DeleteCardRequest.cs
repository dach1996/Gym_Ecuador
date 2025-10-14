using LogicApi.Model.Response;
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
    public ContextRequest ContextRequest { get; set; }
}