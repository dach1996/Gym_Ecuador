using LogicWebJob.Model.Request.Seat;
using Common.Utils.Extensions;
using Common.WebCommon.Models.WebSocketApi.Event;
using LogicCommon.Model.Request.Event;
using LogicCommon.Model.Common.Hub;
using LogicCommon.Model.Enum;
using PersistenceDb.Models.Core;
namespace LogicWebJob.BusinessLogic.SeatHandler;

/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class VoidSeatReservationHandler(
    ILogger<VoidSeatReservationHandler> logger,
    IPluginFactory pluginFactory) : SeatBase<VoidSeatReservationRequest, Unit>(logger, pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<Unit> Handle(VoidSeatReservationRequest request, CancellationToken cancellationToken)
     => await ExecuteHandlerAsync(async () =>
     {
         var dateTimeSearch = Now.AddDays(-2);
         //Obtiene los asientos a cancelar la reserva
         var seatsToVoid = await CoreUnitOfWork.ReserveSeatRepository.GetGenericAsync(
            select => new
            {
                RouteGuid = select.CooperativeRoute.Guid,
                select.Id,
                select.UserId,
                select.RowControl,
                SeatGuid = select.Guid,
                FloorCooperativeGuid = select.FloorDiagramBusCooperative.Guid,
                select.SeatIdentifier,
                select.QueueMessageId
            },
            where => where.UserId == request.UserId
                && where.State == EnumsDataBase.SeatState.Reserved
                && !request.ExcludeRouteGuids.Contains(where.CooperativeRoute.Guid)
                && where.CooperativeRoute.DateTimeRouteTime > dateTimeSearch
         ).ConfigureAwait(false) ?? [];

         //Verifica si existen asientos encontrados
         if (!seatsToVoid.IsNullOrEmpty())
         {
             //Genera la lista de items para agregar al historial
             var seatsUpdated = seatsToVoid.Select(seat => seat).ToList();
             //Recorre la lista de asientos para actualizar los datos
             foreach (var seat in seatsToVoid)
             {
                 var wasUpdated = (await CoreUnitOfWork.ReserveSeatRepository.UpdateByAsync(update => new ReserveSeat
                 {
                     State = EnumsDataBase.SeatState.Available,
                     RowControl = Now,
                     DateTimeExpiration = null,
                     UserId = null,
                     QueueMessageId = null,
                 }, where =>
                    where.Id == seat.Id
                    && where.RowControl == seat.RowControl
                    && where.UserId == request.UserId
                    ).ConfigureAwait(false)) > 0;
                 if (!wasUpdated)
                 {
                     Logger.LogWarning("No se pudo cancelar la reserva del asiento con Id: '{@SeatId}' del usuario con Id: '{@UserId}'", seat.Id, seat.UserId);
                     seatsUpdated.RemoveAll(item => item.Id == seat.Id);
                 }
             }
             //Verifica si existen items para agregar al historial
             if (!seatsUpdated.IsNullOrEmpty())
             {
                 Logger.LogInformation("La reserva de asientos con Id: '{@SeatIds}' fueron canceladas", seatsUpdated.Select(t => t.Id).Join());
                 //Elimina el queue de reserva que no exista nen la otra lista
                 seatsToVoid.RemoveAll(seat => seatsUpdated.TrueForAll(item => item.Id != seat.Id));
                 var queueMessages = seatsToVoid.Select(select => select.QueueMessageId).Where(where => where.HasValue).ToList();
                 if (!queueMessages.IsNullOrEmpty())
                     await DeleteQueueMessageWithoutResponseAsync(where => queueMessages.Contains(where.Id)).ConfigureAwait(false);
                 var updateSeatHubModelsGroups = seatsToVoid
                .GroupBy(groupBy => groupBy.RouteGuid)
                .Select(select => new
                {
                    RouteGuid = select.Key,
                    UpdateSeats = select.Select(seat => new UpdateSeatItemHubModel(
                         seat.RouteGuid,
                         seat.FloorCooperativeGuid,
                         seat.SeatGuid,
                         seat.SeatIdentifier,
                         $"{SeatState.Available}"
                         ))
                });
                 //Agrupa por ticket y envía una lista de asientos a actaulizar
                 foreach (var group in updateSeatHubModelsGroups)
                     _ = Mediator.Send(new NotifyGroupEventRequest(
                        EventHubName.UpdateSeatEvent,
                        group.RouteGuid.ToString(),
                        new UpdateSeatHubModel(group.UpdateSeats),
                        CommonContextRequest)).ConfigureAwait(false);
             }
         }
         return Unit.Value;
     }, EnumsDataBase.UnitOfWorkType.Core).ConfigureAwait(false);
}