namespace LogicApi.Model.Response.Card;
/// <summary>
/// Respuesta de obtener mis tarjetas registradas
/// </summary>
public class GetMyCardsResponse : IApiBaseResponse
{
    /// <summary>
    /// Items de Tarjeta
    /// </summary>
    /// <value></value>
    public IEnumerable<CardItem> CardItems { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="cardItems"></param>
    public GetMyCardsResponse(IEnumerable<CardItem> cardItems) => CardItems = cardItems;

    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    /// <value></value>
    public bool ShowMessage { get; set; }
}
