using LogicApi.Model.Request.Card;
using LogicApi.Model.Response.Card;
namespace LogicApi.BusinessLogic.CardHandler;

/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetMyCardsHandler(
    ILogger<GetMyCardsHandler> logger,
    IPluginFactory pluginFactory) : CardBase<GetMyCardsRequest, GetMyCardsResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetMyCardsResponse> Handle(GetMyCardsRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetMyCards, request, async () =>
        {
            var cards = (await AuthenticationUnitOfWork.CardRepository
                .GetGenericAsync(
                    select => new
                    {
                        select.Id,
                        select.Pan,
                        select.Bin,
                        select.CardOwner,
                        select.CardLength,
                        select.CardType,
                        select.CardBrand
                    },
                    where => where.UserId == UserId && !where.IsDelete
                ).ConfigureAwait(false))
                .Select(select => new CardItem(
                    select.Id,
                    $"{select.Bin}{string.Empty.PadRight(select.CardLength - select.Bin.Length - select.Pan.Length, 'X')}{select.Pan}",
                    select.CardOwner,
                   (EnumCommon.CardType)select.CardType,
                   (EnumCommon.CardBrand)select.CardBrand
                ));
            return new GetMyCardsResponse(cards);
        }, UnitOfWorkType.Authentication);

}