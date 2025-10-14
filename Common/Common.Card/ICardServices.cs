using Common.Card.Models.Request;
using Common.Card.Models.Response;

namespace Common.Card;
/// <summary>
/// Servicios de Tarjetas
/// </summary>
public interface ICardServices
{
    /// <summary>
    /// Verificar bin
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<VerifyBinResponse> VerifyBinAsync(VerifyBinRequest request);
}