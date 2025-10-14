using LogicApi.Model.Request.Card;
using LogicApi.Model.Response.Card;
namespace LogicApi.BusinessLogic.CardHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class ValidateCardHandler(
    ILogger<ValidateCardHandler> logger,
    IPluginFactory pluginFactory) : CardBase<ValidateCardRequest, ValidateCardResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<ValidateCardResponse> Handle(ValidateCardRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.ValidateCard, request, async () =>
        {
            //Valida el número de Tarjeta
            var bin = ValidateBinCardNumber(request.CardNumber);
            _ = ValidatePanNumber(request.CardNumber);
            //Verifica el bin de la tarjeta
            var verifyBinResponse = await CardServices.VerifyBinAsync(new(bin)).ConfigureAwait(false);
            //Validación de Tarjeta
            return new ValidateCardResponse((EnumCommon.CardType)verifyBinResponse.CardType, (EnumCommon.CardBrand)verifyBinResponse.CardBrand);
        });


}