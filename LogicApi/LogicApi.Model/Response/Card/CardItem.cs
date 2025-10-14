
using Common.WebCommon.Models.Enum;

namespace LogicApi.Model.Response.Card;
/// <summary>
/// Item de Tarjeta
/// </summary>
public class CardItem
{
    /// <summary>
    /// Id de tarjeta
    /// </summary>
    /// <value></value>
    public int CardId { get; set; }

    /// <summary>
    /// Número de tarjeta enmascarado
    /// </summary>
    /// <value></value>
    public string CardMask { get; set; }

    /// <summary>
    /// Nombre de Propietario de Tarjeta
    /// </summary>
    /// <value></value>
    public string CardOwner { get; set; }

    /// <summary>
    /// Imagen de marca de tarjeta
    /// </summary>
    /// <value></value>
    public string UrlBrandImage { get; set; }

    /// <summary>
    ///  Tipo de Tarjeta
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
    /// <param name="cardId"></param>
    /// <param name="cardMask"></param>
    /// <param name="cardOwner"></param>
    /// <param name="cardType"></param>
    /// <param name="cardBrand"></param>
    /// <param name="urlBrandImage"></param>
    public CardItem(int cardId, string cardMask, string cardOwner, CardType cardType, CardBrand cardBrand, string urlBrandImage = "")
    {
        CardId = cardId;
        CardMask = cardMask;
        UrlBrandImage = urlBrandImage;
        CardOwner = cardOwner;
        CardType = cardType;
        CardBrand = cardBrand;
    }
}