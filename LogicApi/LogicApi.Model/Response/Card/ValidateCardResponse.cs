using Common.WebCommon.Models.Enum;

namespace LogicApi.Model.Response.Card;
/// <summary>
/// Respuesta de validar tarjetas
/// </summary>
public class ValidateCardResponse : IApiBaseResponse
{
    /// <summary>
    /// Tipo de Tarjeta
    /// </summary>
    /// <value></value>
    public CardType CardType { get; set; }

    /// <summary>
    /// Marca de Tarjeta
    /// </summary>
    /// <value></value>
    public CardBrand CardBrand { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="cardType"></param>
    /// <param name="cardBrand"></param>
    public ValidateCardResponse(CardType cardType, CardBrand cardBrand)
    {
        CardType = cardType;
        CardBrand = cardBrand;
    }

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
