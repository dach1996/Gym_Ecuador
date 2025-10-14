using LogicApi.Model.Request.Order;
using PersistenceDb.Models.Core;

namespace LogicApi.BusinessLogic.OrderHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class ManualCancelOrderHandler(
    ILogger<ManualCancelOrderHandler> logger,
    IPluginFactory pluginFactory) : OrderBase<ManualCancelOrderRequest, HandlerResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<HandlerResponse> Handle(ManualCancelOrderRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.ManualCancelOrder, request, async () =>
        {
            //Consulta la Orden
            var order = (await CoreUnitOfWork.OrderRepository.GetFirstOrDefaultGenericAsync(
                select => new
                {
                    select.Id,
                    select.UserId,
                    select.State,
                    select.QueueMessageId,
                    select.RowControl,
                    Seats = select.OrderSeatPeople.Select(selectSeat => new
                    {
                        selectSeat.ReserveSeat.Id,
                        selectSeat.ReserveSeat.RowControl
                    })
                },
                where => where.Guid == request.OrderGuid).ConfigureAwait(false))
                ?? throw new CustomException((int)MessagesCodesError.OrderNotFound, $"No se encontró la orden con Id: {request.OrderGuid}");
            //Verifica el usuario
            if (order.UserId != UserId)
                throw new CustomException((int)MessagesCodesError.OrderDontBelongUser, $"La orden con Id: {order.Id} pertenece el usuario: {order.UserId}");
            //Verifica que la orden no esté ya cancelada
            if (await CoreUnitOfWork.OrderCancelationRepository.ExistAnyAsync(where => where.Id == order.Id).ConfigureAwait(false))
                throw new CustomException((int)MessagesCodesError.OrderNotAllowCancel, $"La orden con Id: {order.Id} ya ha sido cancelada anteriormente.");
            //Verifica que la orden no esté pagada o expirada
            if (new[] { OrderState.Paid, OrderState.Cancelated, OrderState.Expired }.Contains(order.State))
                throw new CustomException((int)MessagesCodesError.OrderNotAllowCancel, $"No se puede Cancelar la orden con Id: {order.Id} porque ya ha sido Pagada o Expirada");
            //Elimina el queue de expiración de Orden
            if (order.QueueMessageId.HasValue)
                _ = await DeleteQueueMessageAsync(where => where.Id == order.QueueMessageId).ConfigureAwait(false);
            //Actualiza la orden
            if (await CoreUnitOfWork.OrderRepository.UpdateByAsync(update => new Order
            {
                State = OrderState.Cancelated,
                QueueMessageId = null,
                RowControl = Now,
                LastDateTimeUpdate = Now,
            }, where => where.Id == order.Id && where.RowControl == order.RowControl).ConfigureAwait(false) == 0)
                throw new CustomException((int)MessagesCodesError.OrderNotAllowCancel, $"No se pudo actualizar la orden con Id: {order.Id}");
            //Actualiza asientos
            foreach (var seat in order.Seats)
                await CoreUnitOfWork.ReserveSeatRepository.UpdateByAsync(update => new ReserveSeat
                {
                    State = SeatState.Available,
                    DateTimeExpiration = null,
                    UserId = null,
                    RowControl = Now,
                }, where => where.Id == seat.Id && where.RowControl == seat.RowControl).ConfigureAwait(false);
            //Actualiza la orden y los asientos
            _ = await CoreUnitOfWork.OrderCancelationRepository.AddAsync(new OrderCancelation
            {
                Id = order.Id,
                DateTime = Now,
                Reason = request.Reason ?? string.Empty,
                Type = OrderCancelationType.Manual,
            }, false).ConfigureAwait(false);
            //Retorna respuesta
            return SuccessMessage(MessagesCodesSucess.EmptyMessage);
        }, UnitOfWorkType.Core, registerLogAudit: true);
}
