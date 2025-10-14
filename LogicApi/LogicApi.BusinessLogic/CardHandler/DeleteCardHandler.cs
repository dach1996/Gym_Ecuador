using LogicApi.Model.Request.Card;
namespace LogicApi.BusinessLogic.CardHandler;

public class DeleteCardHandler(
    ILogger<DeleteCardHandler> logger,
    IPluginFactory pluginFactory) : CardBase<DeleteCardRequest, HandlerResponse>(
        logger,
        pluginFactory)
{
    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<HandlerResponse> Handle(DeleteCardRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.DeleteCard, request, async () =>
        {
            //Busca la tarjeta
            var card = await AuthenticationUnitOfWork.CardRepository
                .GetByFirstOrDefaultAsync(where => where.Id == request.CardId).ConfigureAwait(false)
                ?? throw new CustomException((int)MessagesCodesError.CardNotFound, $"La tarjeta con Id: '{request.CardId}' no fué encontrada");
            //Tarjeta ha sido eliminada?
            if (card.IsDelete)
                throw new CustomException((int)MessagesCodesError.CardNotFound, $"La tarjeta con Id: '{request.CardId}' ya ha sido eliminada");
            //Tarjeta pertenece a otro usuario
            if (card.UserId != UserId)
                throw new CustomException((int)MessagesCodesError.CardNotFound, $"La tarjeta con Id: '{request.CardId}' no pertenece al usuario: '{UserId}'");
            card.IsDelete = true;
            //Eliminar la tarjeta
            await AuthenticationUnitOfWork.CardRepository.UpdateAsync(card).ConfigureAwait(false);
            //Retorna respuesta
            return SuccessMessage(MessagesCodesSucess.CardDeleteSuccess);
        }, UnitOfWorkType.Authentication);


}