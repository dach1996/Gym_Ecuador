using LogicWebJob.Model.Request.Seat;
using Common.Utils.Extensions;
using LogicCommon.Model.Request.Event;
using Common.WebCommon.Models.WebSocketApi.Event;
using LogicCommon.Model.Common.Hub;
using LogicCommon.Model.Enum;
using Common.WebCommon.Templates.Notification.Model;
using PersistenceDb.Models.Core;
namespace LogicWebJob.BusinessLogic.SeatHandler;

/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class ExpireSeatHandler(
    ILogger<ExpireSeatHandler> logger,
    IPluginFactory pluginFactory) : SeatBase<ExpireSeatRequest, Unit>(logger, pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<Unit> Handle(ExpireSeatRequest request, CancellationToken cancellationToken)
     => await ExecuteHandlerAsync(async () =>
     {
         //Buscamos los registros con los Ids y en estado reservado para poderlos expirar 
         var seats = await CoreUnitOfWork.ReserveSeatRepository.GetGenericAsync
            (
                select => new
                {
                    select.Id,
                    select.UserId,
                    select.RowControl,
                    RouteGuid = select.CooperativeRoute.Guid,
                    FloorBusCooperativeGuid = select.FloorDiagramBusCooperative.Guid,
                    SeatGuid = select.Guid,
                    select.SeatIdentifier
                },
                where => request.SeatIds.Contains(where.Id)
                && where.QueueMessage.InternlaIdentifier == request.WebJobContext.InternalIdentifier
                && where.State == EnumsDataBase.SeatState.Reserved
                && where.QueueMessageId > 0

            ).ConfigureAwait(false) ?? [];
         //Genera la lista de items para agregar al historial
         var seatsUpdated = seats.Select(seat => seat).ToList();
         foreach (var seat in seats)
         {
             var wasUpdated = (await CoreUnitOfWork.ReserveSeatRepository.UpdateByAsync(update => new ReserveSeat
             {
                 State = EnumsDataBase.SeatState.Available,
                 RowControl = Now,
                 DateTimeExpiration = null,
                 UserId = null,
                 QueueMessageId = null,
             }, where => where.Id == seat.Id && where.RowControl == seat.RowControl).ConfigureAwait(false)) > 0;
             //Verifica si se pudo actualizar el asiento
             if (!wasUpdated)
             {
                 Logger.LogWarning("No se pudo expirar el asiento con Id: '{@SeatId}' del usuario con Id: '{@UserId}'", seat.Id, seat.UserId);
                 seatsUpdated.RemoveAll(item => item.Id == seat.Id);
             }
         }
         //Verifica si la información está vacía 
         if (!seatsUpdated.IsNullOrEmpty())
         {
             Logger.LogInformation("La reserva de asientos con Id: '{@SeatIds}' expiraron", seatsUpdated.Select(s => s.Id).Join());
             //Elimina los asientos que se expiraron
             seats.RemoveAll(seat => seatsUpdated.TrueForAll(item => item.Id != seat.Id));
             //Envía los eventos para actualizar asientos por SignalR
             foreach (var groupSeatIdentifiear in seats.GroupBy(groupBy => groupBy.RouteGuid))
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
             //Envía las notificaciones Push
             foreach (var userIdGroup in seats.Where(where => where.UserId.HasValue).GroupBy(groupBy => groupBy.UserId))
                 //Envía notificación push
                 await SendNotificationAsync(
                    new ExpiredReserveSeatNotificationTemplate(),
                    userIdGroup.Key.Value).ConfigureAwait(false);

         }
         return Unit.Value;
     }, EnumsDataBase.UnitOfWorkType.Core).ConfigureAwait(false);
}