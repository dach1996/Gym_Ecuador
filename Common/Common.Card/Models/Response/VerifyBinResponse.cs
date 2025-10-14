using System.Text.Json.Serialization;

namespace Common.Card.Models.Response;
/// <summary>
/// Respuesta para verificar Bin
/// </summary>
public class VerifyBinResponse
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
    public VerifyBinResponse(CardType cardType, CardBrand cardBrand)
    {
        CardType = cardType;
        CardBrand = cardBrand;
    }
}

internal class BinInformation
{
    /// <summary>
    /// Marca
    /// </summary>
    /// <value></value>
    public string Brand { get; set; }

    /// <summary>
    /// Esquema
    /// </summary>
    /// <value></value>
    public string Scheme { get; set; }

    /// <summary>
    /// Tipo
    /// </summary>
    /// <value></value>
    public string Type { get; set; }

    /// <summary>
    /// Nivel
    /// </summary>
    /// <value></value>
    public string Level { get; set; }

}

/// <summary>
/// Respuesta de verificación Interna
/// </summary>
internal class InternalVerifyBinResponse
{
    /// <summary>
    /// Bin de respuesta
    /// </summary>
    /// <value></value>
    [JsonPropertyName("BIN")]
    public BinInformation Bin { get; set; }
}