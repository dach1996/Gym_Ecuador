using Common.Card;

namespace LogicApi.BusinessLogic.CardHandler;

/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
/// <returns></returns>
public abstract class CardBase<TRequest, TResponse>(
    ILogger<CardBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    protected ICardServices CardServices => PluginFactory.GetPlugin<ICardServices>(AppSettingsApi.CardServicesConfiguration?.CurrentImplementation, true);

    /// <summary>
    /// Valida el número de Tarjeta
    /// </summary>
    /// <param name="cardNumber"></param>
    /// <returns></returns>
    protected static string ValidateBinCardNumber(string cardNumber)
    {
        //número de caracteres inválidos
        if (cardNumber.Length < 14)
            throw new CustomException((int)MessagesCodesError.CardBad, $"La tarjeta ingresada no contiene un número de dígitos válidos.");
        //Tarjeta contiene solo números    
        if (!cardNumber.ContainsOnlyNumbers())
            throw new CustomException((int)MessagesCodesError.CardBad, $"La tarjeta ingresada contiene letras");
        //Bin de la tarejta
        var bin = cardNumber[..6];
        if (bin.IsNullOrEmpty())
            throw new CustomException((int)MessagesCodesError.CardBad, $"Los 6 primeros números de la tarjeta son incorrectos.");
        return bin;
    }

    /// <summary>
    /// Validate  Pan Number
    /// </summary>
    /// <param name="cardNumber"></param>
    /// <returns></returns>
    protected static string ValidatePanNumber(string cardNumber)
    {
        //Pan de la tarjeta
        var pan = cardNumber[^4..];
        if (pan.IsNullOrEmpty())
            throw new CustomException((int)MessagesCodesError.CardBad, $"Últimos 4 números de la tarjeta son incorrectos.");
        return pan;
    }

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
