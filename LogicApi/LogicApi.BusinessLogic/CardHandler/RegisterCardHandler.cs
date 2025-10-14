using System.Net.Cache;
using LogicApi.Model.Request.Card;
using PersistenceDb.Models.Authentication;
namespace LogicApi.BusinessLogic.CardHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class RegisterCardHandler(
    ILogger<RegisterCardHandler> logger,
    IPluginFactory pluginFactory) : CardBase<RegisterCardRequest, HandlerResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<HandlerResponse> Handle(RegisterCardRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.RegisterCard, request, async () =>
        {
            //Valida el número de Tarjeta
            var bin = ValidateBinCardNumber(request.CardNumber);
            var pan = ValidatePanNumber(request.CardNumber);
            //Busca la tarjeta
            var cardIsDelete = await AuthenticationUnitOfWork.CardRepository
                .GetFirstOrDefaultGenericAsync<bool?>(
                    select => select.IsDelete,
                    where => where.UserId == UserId && where.Bin == bin && where.Pan == pan).ConfigureAwait(false);
            //Tarjeta ha sido eliminada
            if (!cardIsDelete ?? false)
                throw new CustomException((int)MessagesCodesError.CardNotFound, $"La tarjeta ingresada ya se encuentra registrada al usuario: '{UserId}'");
            //Verifica el bin de la tarjeta
            var verifyBinResponse = await CardServices.VerifyBinAsync(new(bin)).ConfigureAwait(false);
            //Crea el nuevo registro de Tarjeta
            await AuthenticationUnitOfWork.CardRepository.AddAsync(new()
            {
                UserId = UserId,
                Bin = bin,
                Pan = pan,
                CardBrand = (CardBrand)verifyBinResponse.CardBrand,
                CardType = (CardType)verifyBinResponse.CardType,
                CardOwner = request.CardOwner,
                CardLength = request.CardNumber.Length
            }).ConfigureAwait(false);
            //Registra una tarjeta
            return SuccessMessage(MessagesCodesSucess.CardRegisterSuccess);
        }, UnitOfWorkType.Authentication);

}