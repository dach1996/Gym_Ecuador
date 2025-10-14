using Common.Utils.Extensions;
using Common.WebCommon.Models.WebSocketApi.Event;
using Common.WebCommon.Templates.Notification.Model;
using LogicCommon.Model.Common.Hub;
using LogicCommon.Model.Enum;
using LogicCommon.Model.Request.Event;
using LogicWebJob.Model.Request.Seat;
using PersistenceDb.Models.Core;
namespace LogicWebJob.BusinessLogic.OrderHandler;

/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class ExpiredOrderHandler(
    ILogger<ExpiredOrderHandler> logger,
    IPluginFactory pluginFactory) : OrderBase<ExpiredOrderRequest, Unit>(logger, pluginFactory)
{
    private const string EXPIRED_MESSAGE = "Orden Expirada";

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<Unit> Handle(ExpiredOrderRequest request, CancellationToken cancellationToken)
     => await ExecuteHandlerAsync(async () =>
     {
         //Buscamos los registros con los Ids y en estado reservado para poderlos expirar 
         var orders = await CoreUnitOfWork.OrderRepository.GetGenericAsync
            (
                select => new
                {
                    select.Id,
                    select.QueueMessageId,
                    select.RowControl,
                    select.UserId,
                    Seats = select.OrderSeatPeople.Select(seat => new
                    {
                        Id = seat.ReserveSeatId,
                        seat.ReserveSeat.UserId,
                        seat.ReserveSeat.RowControl,
                    })
                },
                where => request.OrderIds.Contains(where.Id)
                && where.QueueMessage.InternlaIdentifier == request.WebJobContext.InternalIdentifier
                && where.State == EnumsDataBase.OrderState.Created
            ).ConfigureAwait(false);
         if (orders.IsNullOrEmpty())
             Logger.LogWarning("No se encontraron órdenes para expirar con el Id: '{@OrderIds}'", request.OrderIds.Join());
         //Realiza el proceso de expiración por órden ya que es un paquete completo de asientos 
         foreach (var order in orders)
         {
             //Actualiza la orden
             var orderWasUpdated = await CoreUnitOfWork.OrderRepository.UpdateByAsync(update => new Order
             {
                 State = EnumsDataBase.OrderState.Expired,
                 LastDateTimeUpdate = Now,
                 QueueMessageId = null,
             }, where => where.Id == order.Id && where.RowControl == order.RowControl).ConfigureAwait(false);
             //Verifica si se pudo actualizar la orden
             if (orderWasUpdated != 1)
             {
                 Logger.LogWarning("No se pudo actualizar la orden con Id: '{@OrderId}' debido a que el control de fila no coincide", order.Id);
                 continue;
             }
             //Registra la cancelación de la orden
             await CoreUnitOfWork.OrderCancelationRepository.AddAsync(new OrderCancelation
             {
                 Id = order.Id,
                 DateTime = Now,
                 Reason = EXPIRED_MESSAGE,
                 Type = EnumsDataBase.OrderCancelationType.Expiration
             });
             //Recorre cada asiento        
             foreach (var orderSeat in order.Seats)
             {
                 var seatWasUpdated = (await CoreUnitOfWork.ReserveSeatRepository.UpdateByAsync(update => new ReserveSeat
                 {
                     State = EnumsDataBase.SeatState.Available,
                     RowControl = Now,
                     DateTimeExpiration = null,
                     UserId = null,
                     QueueMessageId = null,
                 }, where => where.Id == orderSeat.Id && where.RowControl == orderSeat.RowControl).ConfigureAwait(false)) > 0;
                 //Verifica si se pudo actualizar el asiento
                 if (!seatWasUpdated)
                     Logger.LogWarning("No se pudo expirar el asiento con Id: '{@SeatId}' del usuario con Id: '{@UserId}'", orderSeat.Id, orderSeat.UserId);
             }
             //Envía notificación push
             await SendNotificationAsync(new ExpiredOrderNotificationTemplate($"{order.Id}"), order.UserId).ConfigureAwait(false);
             //Agrega historil de asiento
             var seatsIds = order.Seats.Select(seat => seat.Id).ToList();
             var seatsInformation = await CoreUnitOfWork.ReserveSeatRepository.GetGenericAsync(
                select => new
                {
                    select.RowControl,
                    RouteGuid = select.CooperativeRoute.Guid,
                    FloorBusCooperativeGuid = select.FloorDiagramBusCooperative.Guid,
                    SeatGuid = select.Guid,
                    select.SeatIdentifier
                },
                where => seatsIds.Contains(where.Id)).ConfigureAwait(false);
             //Envía los eventos para actualizar asientos por SignalR
             foreach (var groupSeatIdentifiear in seatsInformation.GroupBy(groupBy => groupBy.RouteGuid))
                 _ = Mediator.Send(new NotifyGroupEventRequest(
                    EventHubName.UpdateSeatEvent,
                    $"{groupSeatIdentifiear.Key}",
                        new UpdateSeatHubModel(groupSeatIdentifiear.Select(seat => new UpdateSeatItemHubModel(
                        seat.RouteGuid,
                        seat.FloorBusCooperativeGuid,
                        seat.SeatGuid,
                        seat.SeatIdentifier,
                        $"{SeatState.Available}"
                        ))),
                    CommonContextRequest)).ConfigureAwait(false);
             Logger.LogWarning("La orden con Id: '{@OrderId}' Expiró y sus asientos: '{@SeatIds}'", order.Id, seatsIds.Join());
         }
         return Unit.Value;
     }, [EnumsDataBase.UnitOfWorkType.Core]).ConfigureAwait(false);

}