using LogicApi.Abstractions.Interfaces.Order.Payment;
using LogicApi.Model.Request.Order.Payment;
using PersistenceDb.Models.Core;
namespace LogicApi.BusinessLogic.OrderHandler.Payment.Card;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class PayphonePaymentOrderCardHandler(
    ILogger<PayphonePaymentOrderCardHandler> logger,
    IPluginFactory pluginFactory) : PaymentOrderCardBase(
        logger,
        pluginFactory), IPaymentOrderCardHandler
{


    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<HandlerResponse> Handle(PaymentOrderByCardRequest request)
      => await ExecuteHandlerAsync(OperationApiName.PaymentOrder, request, async () =>
        {
            //Obtiene la orden
            var order = await CoreUnitOfWork.OrderRepository
                .GetFirstOrDefaultGenericAsync(
                    select => new
                    {
                        select.Id,
                        select.UserId,
                        select.State,
                        select.QueueMessageId,
                        select.RowControl,
                        Seats = select.OrderSeatPeople.Select(select => new
                        {
                            select.Price,
                            select.ReserveSeatId,
                            select.ReserveSeat.RowControl
                        }),
                    }, where => where.Guid == request.OrderGuid).ConfigureAwait(false)
                ?? throw new CustomException((int)MessagesCodesError.OrderNotFound, $"Orden con Id: '{request.OrderGuid}' no fue encontrada");
            //Verifica si la orden ya fue pagada
            if (new[] { OrderState.Cancelated, OrderState.Paid, OrderState.Expired }.Contains(order.State))
                throw new CustomException((int)MessagesCodesError.OrderStateInvalidOperation, $"Orden con Id: '{request.OrderGuid}' se encuntra en un estado inválido: '{order.State}' para el pago.");
            //Verifica que el usuario sea el dueño de la Orden
            if (order.UserId != UserId)
                throw new CustomException((int)MessagesCodesError.DataNotBelongContextUser, $"La orden con Id: '{request.OrderGuid}' no pertece al Usuario con Id: '{order.UserId}'");
            //Agrega el pago de la orden
            var rowUpdate = await CoreUnitOfWork.OrderRepository.UpdateByAsync(update => new Order
            {
                LastDateTimeUpdate = Now,
                QueueMessageId = null,
                State = OrderState.Paid,
                RowControl = order.RowControl
            }, where => where.Id == order.Id && where.RowControl == order.RowControl).ConfigureAwait(false);
            //Verifica si la orden no permite ser pagada
            if (rowUpdate == 0)
                throw new CustomException((int)MessagesCodesError.OrderNotAllowPay, $"La orden con Id: '{request.OrderGuid}' no pertece al Usuario con Id: '{order.UserId}'");
            //Eliminael queue de expiración de órden
            await DeleteQueueMessageAsync(where => where.Id == order.QueueMessageId).ConfigureAwait(false);
            //Actualiza los asientos de la orden
            foreach (var seat in order.Seats)
                await CoreUnitOfWork.ReserveSeatRepository.UpdateByAsync(update => new ReserveSeat
                {
                    State = SeatState.Purchased,
                    RowControl = Now
                }, where => where.Id == seat.ReserveSeatId && where.RowControl == seat.RowControl).ConfigureAwait(false);
            //Agrega el pago de la orden
            await CoreUnitOfWork.OrderPaymentRepository.AddAsync(new OrderPayment
            {
                Id = order.Id,
                DateTimeRegister = Now,
                Price = order.Seats.Sum(select => select.Price),
            }).ConfigureAwait(false);
            //Retorna la respuesta
            return SuccessMessage(MessagesCodesSucess.OrderSeatPaid);
        }, UnitOfWorkType.Core, true);
}
