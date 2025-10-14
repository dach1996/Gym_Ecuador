using Common.Queue.Model.Template;
using LogicApi.Model.Request.Order;
using LogicApi.Model.Response.Order;
using PersistenceDb.Models.Core;

namespace LogicApi.BusinessLogic.OrderHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GenerateOrderHandler(
    ILogger<GenerateOrderHandler> logger,
    IPluginFactory pluginFactory) : OrderBase<GenerateOrderRequest, GenerateOrderResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GenerateOrderResponse> Handle(GenerateOrderRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GenerateOrder, request, async () =>
        {
            var routeInformation = await GetRouteInformationCacheAsync(request.RouteGuid).ConfigureAwait(false);
            await SetCooperativeAsync(routeInformation.CooperativeId).ConfigureAwait(false);
            //Máximo de minutos
            var maxSecondsReservedOrder = await GetIntParameterAsync(ParametersCodes.MaxSecondsReservedOrder).ConfigureAwait(false);
            //Valida el Orgien y Destino
            var seats = await GetPersonSeatInformationAsync(request.RouteGuid, request.SeatPeople).ConfigureAwait(false);
            //Registra nueva orden
            var expiredOrder = Now.AddSeconds(maxSecondsReservedOrder);
            foreach (var seat in seats)
            {
                //TODO:CONTROLAR QUE PASA SI UNO NO ACTUALIZA
                await CoreUnitOfWork.ReserveSeatRepository.UpdateByAsync(update => new ReserveSeat
                {
                    DateTimeExpiration = expiredOrder,
                    State = SeatState.Prepaid,
                    QueueMessageId = null,
                    RowControl = Now
                }, where => where.Id == seat.SeatId && where.RowControl == seat.RowControl).ConfigureAwait(false);
            }
            //Obtiene los ids de los mensajes de cola a expirar
            var seatsQueueExpiredIds = seats.Where(where => where.QueueMessageId is not null)
                .Select(t => t.QueueMessageId).Distinct().ToArray();
            if (!seatsQueueExpiredIds.IsNullOrEmpty())
                //Elimina los mensajes de cola a expirar
                await DeleteQueueMessageWithoutResponseAsync(where => seatsQueueExpiredIds.Contains(where.Id)).ConfigureAwait(false);
            var order = await CoreUnitOfWork.OrderRepository.AddAsync(new Order
            {
                Guid = Guid.NewGuid(),
                CooperativeRouteId = routeInformation.RouteId,
                UserId = UserId,
                DateTimeRegister = Now,
                State = OrderState.Created,
                DateTimeExpiration = expiredOrder,
                LastDateTimeUpdate = Now,
                RowControl = Now,
                OrderSeatPeople = [.. seats.Select(select => new OrderSeatPerson
                {
                    PersonId = select.PersonId,
                    ReserveSeatId = select.SeatId,
                    Price = select.Price
                })]
            }).ConfigureAwait(false);
            //Envía el Queue
            var queueMessage = await SendAndSaveQueueMessageAsync(new ExpiredOrderQueueTemplate(order.Id), maxSecondsReservedOrder).ConfigureAwait(false);
            //Actualiza la Orden
            await CoreUnitOfWork.OrderRepository.UpdateByAsync(order => new Order
            {
                QueueMessageId = queueMessage.Id
            }, where => where.Id == order.Id).ConfigureAwait(false);
            //Retorna la respuesta
            return new GenerateOrderResponse(order.Guid, GetSuccessMessage(MessagesCodesSucess.OrderGenerated));
        }, UnitOfWorkType.Core, true);


}
