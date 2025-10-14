using System.Linq.Expressions;
using Common.Queue.Model.Enum;
using Common.Queue.Model.Template;
using Common.WebCommon.Models.Queues;
using Common.WebCommon.Models.WebSocketApi.Event;
using LogicApi.Model.Common;
using LogicApi.Model.Request.Seat;
using LogicApi.Model.Response.Seat;
using LogicCommon.Model.Common.Hub;
using LogicCommon.Model.Request.Event;
using LogicCommon.Model.Request.Queue;
using PersistenceDb.Models.Core;
namespace LogicApi.BusinessLogic.SeatHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class ReserveSeatHandler(
    ILogger<ReserveSeatHandler> logger,
    IPluginFactory pluginFactory) : SeatBase<ReserveSeatRequest, ReserveSeatResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<ReserveSeatResponse> Handle(ReserveSeatRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.ReserveSeat, request, async () =>
        {
            //Verifica el màximo de asientos permitidos
            var maxSeatReservedAllowDefault = await GetIntParameterAsync(ParametersCodes.MaxSeatReservedAllowDefault).ConfigureAwait(false);
            //Obtiene la información de los asientos relacionados al usuario y reserva el asiento
            var reservedSeatsUser = await GetReservedSeatsInformationByUserAsync(request).ConfigureAwait(false);
            //Busca el asiento en la lista de asientos reservados por el usuario para ver si es nuevo o ya existe, o sinó lo busca en la base de datos con el ticket
            var reserveSeat = await GetCurrentSeatAsync(request, reservedSeatsUser).ConfigureAwait(false);
            //Verifica si el asiento es nuevo o lo encontró en la lista
            var wasUpdateSeat = false;
            var isNewSeat = reserveSeat is null;
            if (isNewSeat)
            {
                reserveSeat = await TryAddNewSeatAsync(request).ConfigureAwait(false);
                reservedSeatsUser.Add(reserveSeat);
                if (reservedSeatsUser.Count > maxSeatReservedAllowDefault)
                    throw new CustomException((int)MessagesCodesError.MaxSeatReserveAllow, $"El usuario excedió el máximo de asientos permitidos: '{maxSeatReservedAllowDefault}'");
                wasUpdateSeat = true;
            }
            //Verifica si el asiento no es nuevo y se debe actualizar el estado del asiento
            else
            {
                //Intenta actualizar el asiento concurrentemente
                var updateSeat = await TryUpdateSeatAsync(maxSeatReservedAllowDefault, reservedSeatsUser, reserveSeat).ConfigureAwait(false);
                //Verifica si se actualizó el asiento
                if (updateSeat is not null)
                {
                    wasUpdateSeat = true;
                    reserveSeat = updateSeat;
                }
            }
            var userGuid = reserveSeat.UserId.HasValue
                ? (await GetUserInformationByUserIdAsync([reserveSeat.UserId.Value]).ConfigureAwait(false))[reserveSeat.UserId.Value].Guid
                : Guid.Empty;

            //Verifica si pudo actualizar el asiento
            if (wasUpdateSeat)
            {
                //Envía notificación de Hub
                _ = Mediator.Send(new NotifyGroupEventRequest(
                    EventHubName.UpdateSeatEvent,
                    $"{request.CooperativeRouteGuid}",
                            new UpdateSeatHubModel(
                                    reserveSeat.CooperativeRouteGuid,
                                    reserveSeat.FloorBusCooperativeGuid,
                                    reserveSeat.SeatGuid,
                                    reserveSeat.SeatIdentifier,
                                    $"{(EnumLogicCommon.SeatState)reserveSeat.State}",
                                    userGuid
                                    ),
                            ContextRequest)).ConfigureAwait(false);
                Logger.LogInformation("Asiento {@SeatIdentifier} marcado como: {@State}", reserveSeat.SeatIdentifier, reserveSeat.State);
                // Busca los messageIds en caso de que el asiento sea actualmente reservado
                if (reserveSeat.State == SeatState.Reserved)
                {
                    await DeleteReservedSeatQueueMessageIdAsync(reservedSeatsUser).ConfigureAwait(false);
                    await SendExpireSeatQueueMessageAsync(reservedSeatsUser).ConfigureAwait(false);
                }
            }
            return new ReserveSeatResponse
            {
                SeatNewState = new SeatNewStateModel(
                        reserveSeat.CooperativeRouteGuid,
                        reserveSeat.FloorBusCooperativeGuid,
                        reserveSeat.SeatGuid,
                        (EnumLogicCommon.SeatState)reserveSeat.State,
                        reserveSeat.SeatIdentifier,
                        userGuid)
            };
        }, UnitOfWorkType.Core);

    /// <summary>
    /// Envía y guarda el mensaje de cola para expirar los asientos
    /// </summary>
    /// <param name="reservedSeatsUser"></param>
    /// <returns></returns>
    private async Task SendExpireSeatQueueMessageAsync(List<SeatInformation> reservedSeatsUser)
    {
        //Obtiene el máximo de segundos para reservar un asiento
        var maxSecodsReservedSeat = await GetIntParameterAsync(ParametersCodes.MaxSecondsReservedSeat).ConfigureAwait(false);
        //Envía y guarda el mensaje de cola para expirar los asientos
        var queueMessageId = (await SendAndSaveQueueMessageAsync(
                new ExpiredSeatQueueTemplate(reservedSeatsUser.Select(t => t.SeatId)),
                maxSecodsReservedSeat,
                Now
            ).ConfigureAwait(false)).Id;
        var dateTimeExpiration = Now.AddSeconds(maxSecodsReservedSeat);
        var ids = reservedSeatsUser.Select(select => select.SeatId);
        await CoreUnitOfWork.ReserveSeatRepository.UpdateByAsync(
             update => new ReserveSeat
             {
                 QueueMessageId = queueMessageId,
                 DateTimeExpiration = dateTimeExpiration,
             },
             where => ids.Contains(where.Id)
            ).ConfigureAwait(false);
    }

    /// <summary>
    /// Elimina los queue messages Ids de los asientos
    /// </summary>
    /// <param name="reservedSeatsUser"></param>
    /// <returns></returns>
    private async Task DeleteReservedSeatQueueMessageIdAsync(List<SeatInformation> reservedSeatsUser)
    {
        var queueMessagesIdsToRemove = reservedSeatsUser.Select(t => t.QueueMessageId).Where(where => where.HasValue);
        if (!queueMessagesIdsToRemove.IsNullOrEmpty())
        {
            var deleteMessageQueueItems = (await CoreUnitOfWork.QueueMessageRepository.GetGenericAsync(
                           select => new
                           {
                               select.AdditionalInformation,
                               select.Type
                           },
                           where => queueMessagesIdsToRemove.Contains(where.Id)
                       ).ConfigureAwait(false))
                           ?.Select(select => new
                           {
                               queueMessage = select,
                               sendMessageQueueAzureResponse = select.AdditionalInformation.ToObject<SendMessageQueueAzureResponse>()
                           })
                       .Select(select => new DeleteMessageQueueItem((QueueTemplateName)select.queueMessage.Type, select.sendMessageQueueAzureResponse.MessageId, select.sendMessageQueueAzureResponse.PopReceipt));
            _ = Mediator.Send(new DeleteMessageQueueRequest(deleteMessageQueueItems, CommonContextRequest)).ConfigureAwait(false);

        }
    }

    /// <summary>
    /// Obtiene el asiento actual
    /// </summary>
    /// <param name="request"></param>
    /// <param name="reservedSeatsUser"></param>
    /// <returns></returns>
    private async Task<SeatInformation> GetCurrentSeatAsync(
        ReserveSeatRequest request,
        List<SeatInformation> reservedSeatsUser)
    {
        //Obtiene el asiento actual
        var seat = reservedSeatsUser.FirstOrDefault(where => where.SeatIdentifier == request.SeatIdentifier);
        if (seat is null)
        {
            //Intenta obtener el asiento actual de la memoria caché
            var seatId = await AdministratorCache.TryGetAsync<int?>(CacheCodes.SeatIdByData(request.CooperativeRouteGuid, request.FloorBusCooperativeGuid, request.SeatIdentifier)).ConfigureAwait(false);
            //Obtiene la expresión para consulta de asiento
            var routeInformation = await GetRouteInformationCacheAsync(request.CooperativeRouteGuid).ConfigureAwait(false);
            var cooperativeData = (await GetCooperativeDataAsync().ConfigureAwait(false)).GetCooperativeDataById(routeInformation.CooperativeId);
            Expression<Func<ReserveSeat, bool>> whereExpression = seatId.HasValue
                ? where => where.Id == seatId.Value
                : GetWhereExpressionSeat(request, routeInformation.RouteId, cooperativeData);
            //Obtiene el asiento actual
            var currentSeat = await CoreUnitOfWork.ReserveSeatRepository.GetFirstOrDefaultGenericAsync(
                select => new SeatInformation(
                    select.Id,
                    select.RowControl,
                    request.CooperativeRouteGuid,
                    cooperativeData.GetFloorGuidById(select.FloorBusCooperativeId),
                    select.Guid,
                    select.State,
                    select.SeatIdentifier,
                    select.UserId,
                    select.DateTimeExpiration,
                    select.QueueMessageId
                ),
                whereExpression
            ).ConfigureAwait(false);
            //Agrega el asiento a la memoria caché
            seat = currentSeat;
            if (currentSeat is not null)
            {
                _ = AdministratorCache.SetAsync(
                 CacheCodes.SeatIdByData(request.CooperativeRouteGuid, request.FloorBusCooperativeGuid, request.SeatIdentifier), seat.SeatId);
                reservedSeatsUser.Add(seat);
            }
        }
        return seat;
    }

    /// <summary>
    /// Obtiene la información de los asientos relacionados al usuario
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private async Task<List<SeatInformation>> GetReservedSeatsInformationByUserAsync(ReserveSeatRequest request)
    {
        var routeInformation = await GetRouteInformationCacheAsync(request.CooperativeRouteGuid).ConfigureAwait(false);
        var cooperativeData = (await GetCooperativeDataAsync().ConfigureAwait(false)).GetCooperativeDataById(routeInformation.CooperativeId);
        //Obtiene la información de los asientos relacionados al usuario
        return await CoreUnitOfWork.ReserveSeatRepository.GetGenericAsync(
            select => new SeatInformation(
                select.Id,
                select.RowControl,
                request.CooperativeRouteGuid,
                cooperativeData.GetFloorGuidById(select.FloorBusCooperativeId),
                select.Guid,
                select.State,
                select.SeatIdentifier,
                select.UserId,
                select.DateTimeExpiration,
                select.QueueMessageId
            ),
            where =>
            where.CooperativeRouteId == routeInformation.RouteId
            && where.UserId == UserId
            && where.State == SeatState.Reserved
        ).ConfigureAwait(false);
    }

    /// <summary>
    /// Intenta agregar un nuevo asiento
    /// </summary>
    /// <param name="request"></param>
    /// <param name="expiredDate"></param>
    /// <returns></returns>
    private async Task<SeatInformation> TryAddNewSeatAsync(
        ReserveSeatRequest request)
    {
        //Máximo de minutos
        var maxSecodsReservedSeat = await GetIntParameterAsync(ParametersCodes.MaxSecondsReservedSeat).ConfigureAwait(false);
        var expiredDate = Now.AddSeconds(maxSecodsReservedSeat);
        var routeInformation = await GetRouteInformationCacheAsync(request.CooperativeRouteGuid).ConfigureAwait(false);
        var cooperativeData = (await GetCooperativeDataAsync().ConfigureAwait(false)).GetCooperativeDataById(routeInformation.CooperativeId);
        //Obtiene las implementaciones de cooperativas
        var newEntity = new ReserveSeat
        {
            Guid = Guid.NewGuid(),
            CooperativeRouteId = routeInformation.RouteId,
            FloorBusCooperativeId = cooperativeData.GetFloorIdByGuid(request.FloorBusCooperativeGuid),
            DateTimeRegister = Now,
            RowControl = Now,
            UserId = UserId,
            State = SeatState.Reserved,
            SeatIdentifier = request.SeatIdentifier,
            DateTimeExpiration = expiredDate
        };
        newEntity = await CoreUnitOfWork.ReserveSeatRepository.TryAddOrGetFirstAsync(
            newEntity,
            GetWhereExpressionSeat(request, routeInformation.RouteId, cooperativeData)).ConfigureAwait(false);
        //Agrega el asiento a la memoria caché
        _ = AdministratorCache.SetAsync(
            CacheCodes.SeatIdByData(request.CooperativeRouteGuid, request.FloorBusCooperativeGuid, request.SeatIdentifier), newEntity.Id);
        var newSeat = new SeatInformation(
            newEntity.Id,
            newEntity.RowControl,
            routeInformation.RouteGuid,
            cooperativeData.GetFloorGuidById(newEntity.FloorBusCooperativeId),
            newEntity.Guid,
            newEntity.State,
            newEntity.SeatIdentifier,
            newEntity.UserId,
            newEntity.DateTimeExpiration,
            newEntity.QueueMessageId
        );
        ValidateSeatState(newSeat);
        return newSeat;
    }

    /// <summary>
    /// Intenta actualizar el asiento
    /// </summary>
    /// <param name="request"></param>
    /// <param name="maxSeatReservedAllowDefault"></param>
    /// <param name="reservedSeatsUser"></param>
    /// <param name="reserveSeat"></param>
    /// <returns></returns>
    private async Task<SeatInformation> TryUpdateSeatAsync(
        int maxSeatReservedAllowDefault,
        List<SeatInformation> reservedSeatsUser,
        SeatInformation reserveSeat)
    {
        //Verifica si el asiento está disponible y el usuario excedió el máximo de asientos permitidos con el total de asientos reservados + 1 (El asiento en cuestión)
        if (reserveSeat.State == SeatState.Available && reservedSeatsUser.Count + 1 > maxSeatReservedAllowDefault)
            throw new CustomException((int)MessagesCodesError.MaxSeatReserveAllow, $"El usuario excedió el máximo de asientos permitidos: '{maxSeatReservedAllowDefault}'");
        ValidateSeatState(reserveSeat);
        //Cuando el asiento está en un estado disponible entonces cambia a reservado
        SeatInformation newSeatInformation = null;
        if (SeatState.Available == reserveSeat.State)
        {
            var maxSecodsReservedSeat = await GetIntParameterAsync(ParametersCodes.MaxSecondsReservedSeat).ConfigureAwait(false);
            var expiredDate = Now.AddSeconds(maxSecodsReservedSeat);
            newSeatInformation = new SeatInformation(
                         reserveSeat.SeatId,
                         Now,
                         reserveSeat.CooperativeRouteGuid,
                         reserveSeat.FloorBusCooperativeGuid,
                         reserveSeat.SeatGuid,
                         SeatState.Reserved,
                         reserveSeat.SeatIdentifier,
                         UserId,
                         expiredDate,
                         reserveSeat.QueueMessageId
                       );
        }
        //Cuando el asiento está en un estado reservado entonces cambia a disponible
        else if (reserveSeat.State == SeatState.Reserved)
            newSeatInformation = new SeatInformation(
                reserveSeat.SeatId,
                Now,
                reserveSeat.CooperativeRouteGuid,
                reserveSeat.FloorBusCooperativeGuid,
                reserveSeat.SeatGuid,
                SeatState.Available,
                reserveSeat.SeatIdentifier,
                null,
                null,
                reserveSeat.QueueMessageId
            );
        //Intenta actualizar el asiento
        var wasUpdateSeat = await CoreUnitOfWork.ReserveSeatRepository.UpdateByAsync(seat => new ReserveSeat
        {
            State = newSeatInformation.State,
            DateTimeExpiration = newSeatInformation.DateTimeExpired,
            UserId = newSeatInformation.UserId,
            QueueMessageId = null,
            RowControl = Now,
        }, where => where.Id == reserveSeat.SeatId && where.RowControl == reserveSeat.RowControl
        , autoDetectChangesEnabled: false
        ).ConfigureAwait(false);
        //Retorna el asiento actualizado en caso de que se haya actualizado
        return wasUpdateSeat > 0 ? newSeatInformation : null;
    }

    /// <summary>
    /// Valida el estado del asiento
    /// </summary>
    /// <param name="reserveSeat"></param>
    private void ValidateSeatState(SeatInformation reserveSeat)
    {
        //Verifica si el asiento ha sido comprado
        if (reserveSeat.State == SeatState.Purchased)
            throw new CustomException((int)MessagesCodesError.SeatPurchased, $"El con Id '{reserveSeat.SeatId}' e identificador '{reserveSeat.SeatIdentifier}' fué comprado por el Usuario: '{(reserveSeat.UserId.HasValue ? $"{reserveSeat.UserId}" : "Estación")}'");
        //Verifica si el asiento prepagado aún no exipira el tiempo
        if (reserveSeat.State == SeatState.Prepaid)
            throw new CustomException((int)MessagesCodesError.SeatPurchased, $"El con Id '{reserveSeat.SeatId}' e identificador '{reserveSeat.SeatIdentifier}' está incluido en orden de compra.");
        //Verifica si Id del usuario en contexto corresponde al asiento reservado
        if (reserveSeat.UserId != UserId && reserveSeat.State == SeatState.Reserved)
            throw new CustomException((int)MessagesCodesError.SeatReserved, $"El con Id '{reserveSeat.SeatId}' e identificador '{reserveSeat.SeatIdentifier}' está reservado al usuario: '{reserveSeat.UserId}'");
    }

    /// <summary>
    /// Obtiene la expresión para consulta de asiento
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private static Expression<Func<ReserveSeat, bool>> GetWhereExpressionSeat(ReserveSeatRequest request, int routeId, CooperativeItemData cooperativeData)
    {
        var floorId = cooperativeData.GetFloorIdByGuid(request.FloorBusCooperativeGuid);
        return where =>
        where.CooperativeRouteId == routeId
        && where.SeatIdentifier == request.SeatIdentifier
        && where.FloorBusCooperativeId == floorId;
    }
}

/// <summary>
/// Información de actualización de asiento
/// </summary>
/// <param name="SeatId"></param>
/// <param name="RowControl"></param>
/// <param name="TicketIdentifier"></param>  
/// <param name="State"></param>
/// <param name="SeatIdentifier"></param>
/// <param name="UserId"></param>
/// <param name="DateTimeExpired"></param>
/// <param name="QueueMessageId"></param>
public sealed record SeatInformation(
    int SeatId,
    DateTime RowControl,
    Guid CooperativeRouteGuid,
    Guid FloorBusCooperativeGuid,
    Guid SeatGuid,
    SeatState State,
    string SeatIdentifier,
    int? UserId,
    DateTime? DateTimeExpired,
    int? QueueMessageId);


/// <summary>
/// Clave para identificar un asiento
/// </summary>
/// <param name="TicketIdentifier"></param>
/// <param name="CooperativeId"></param>
/// <param name="BusCooperativeId"></param>
/// <param name="FloorBusCooperativeId"></param>
/// <param name="SeatIdentifier"></param>
/// <returns></returns>
public sealed record SeatKey(
    string TicketIdentifier,
    int CooperativeId,
    int BusCooperativeId,
    int FloorBusCooperativeId,
    string SeatIdentifier);

