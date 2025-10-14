using LogicApi.Model.Enum;
using LogicApi.Model.Request.Order.Common;

namespace LogicApi.BusinessLogic.OrderHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
/// <returns></returns>
public abstract class OrderBase<TRequest, TResponse>(
    ILogger<OrderBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{

    /// <summary>
    /// Manejador
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene el Estado para la Vista
    /// </summary>
    /// <param name="state"></param>
    /// <param name="dateTimeOrigin"></param>
    /// <returns></returns>
    protected OrderViewState GetOrderViewState(OrderState state, DateTime dateTimeOrigin) => state switch
    {
        OrderState.Created => OrderViewState.PaymentPending,
        OrderState.Cancelated => OrderViewState.Cancel,
        OrderState.Expired => OrderViewState.Expired,
        OrderState.Paid when dateTimeOrigin > Now => OrderViewState.TravelPending,
        OrderState.Paid when dateTimeOrigin < Now => OrderViewState.Complete,
        _ => throw new NotImplementedException($"No existe implementación para el Estado: '{state}' y la fecha de Origen: {dateTimeOrigin}")
    };

    /// <summary>
    /// Valida los datos del request
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected async Task<SeatPersonRecordInformation[]> GetPersonSeatInformationAsync(Guid routeGuid, SeatPersonRequest[] seatPersonRequests)
    {
        //Valida los asientos
        var seatGuids = seatPersonRequests.Select(t => t.SeatGuid);
        var seats = await CoreUnitOfWork.ReserveSeatRepository.GetGenericAsync(
            select => new
            {
                RouteGuid = select.CooperativeRoute.Guid,
                select.CooperativeRoute.CooperativeId,
                select.Id,
                seatGuid = select.Guid,
                select.UserId,
                select.State,
                select.QueueMessageId,
                select.RowControl
            },
            where => seatGuids.Contains(where.Guid)).ConfigureAwait(false);
        //Valida que exista todos los ids de asientos del request
        if (!seatGuids.ContainsAll(seats.Select(t => t.seatGuid), out var listSeatsNotMatch))
            throw new CustomException((int)MessagesCodesError.OrderModelError, $"No se encontraron los asientos reservados con Ids: '{listSeatsNotMatch.Join()}'.");
        if (seats.Distinct(where => where.RouteGuid).GroupBy(where => where).Any(where => where.Count() > 1, out var routerGuiidsRepeat))
            throw new CustomException((int)MessagesCodesError.OrderModelError, $"Existen asientos a registrar de otras Rutas: '{routerGuiidsRepeat.Join()}' diferentes al Request: '{routeGuid}' ");
        //Valida que los asientos correspondan al ticket del Request
        var routerIds = seats.Distinct(where => where.RouteGuid);
        if (routerIds.Any(where => where != routeGuid, out var routerIdsNotMatch))
            throw new CustomException((int)MessagesCodesError.OrderModelError, $"Existen asientos a registrar de otras Rutas: '{routerIdsNotMatch.Join()}' diferentes al Request: '{routeGuid}' ");
        //Valida que el mismo asiento no pueda ser ocupado varias veces
        if (seatPersonRequests.ExistDuplicates(seat => seat.SeatGuid, out var listSeatIdDuplicates))
            throw new CustomException((int)MessagesCodesError.OrderModelError, $"Los Ids de asientos: '{listSeatIdDuplicates.Join()}' están duplicados");
        //Valida que todos los asientos estén en estado "Reservado"
        if (seats.Any(where => where.State != SeatState.Reserved, out var seatsNotReserved))
            throw new CustomException((int)MessagesCodesError.PlaceNotFound, $"Los asientos con Id: '{seatsNotReserved.Select(t => t.Id).Join()}' no pertenecen a un estado 'Reservado'.");
        //Valida que todos los asientos estén registrados al usuario
        if (seats.Any(where => where.UserId != UserId, out var seatNotMathUserId))
            throw new CustomException((int)MessagesCodesError.PlaceNotFound, $"Los asientos con Id: '{seatNotMathUserId.Select(t => t.Id).Join()}' no fueron registrados por el Usuario {UserId}.");
        //Valida que todos los asientos sean de la misma cooperativa
        var routeInformation = await GetRouteInformationCacheAsync(routeGuid).ConfigureAwait(false);
        if (seats.Any(where => where.CooperativeId != routeInformation.CooperativeId, out var seatsNotMatchCooperative))
            throw new CustomException((int)MessagesCodesError.PlaceNotFound, $"Los asientos a registrar pertenecen a distintas Cooperativas: '{seatsNotMatchCooperative.Select(t => t.Id).Join()}'");
        //Obtiene la información de los asientos
        var personGuids = seatPersonRequests.Select(select => select.PersonGuid).Distinct();
        await TryToConnectAuthenticationnitOfWorkAsync().ConfigureAwait(false);
        var persons = await AuthenticationUnitOfWork.PersonRepository.GetGenericAsync(
            select => new
            {
                select.Guid,
                select.Id
            }, where => personGuids.Contains(where.Guid)).ConfigureAwait(false);
        //Valida que existan todas las personas
        if (!personGuids.ContainsAll(persons.Select(select => select.Guid), out var personNotFound))
            throw new CustomException((int)MessagesCodesError.PersonInformationNotFound, $"No se encontraron las personas con Ids: '{personNotFound.Join()}'");
        //Retorna la información
        return [.. seatPersonRequests.Join(
            seats,
            seat => seat.SeatGuid,
            seat => seat.seatGuid,
            (seat, seatInformation) => new
            {
                seat.PersonGuid,
                seat.Price,
                SeatInformation = seatInformation
            }
        ).Join(
            persons,
            seat => seat.PersonGuid,
            person => person.Guid,
            (seat, person) =>
             new SeatPersonRecordInformation(
                 seat.SeatInformation.Id,
                 seat.SeatInformation.seatGuid,
                 person.Id,
                 person.Guid,
                 seat.Price,
                 seat.SeatInformation.RowControl,
                 seat.SeatInformation.QueueMessageId)
        )];
    }

    /// <summary>
    /// Información de los asientos
    /// </summary>  
    protected sealed record SeatPersonRecordInformation(
        int SeatId,
        Guid SeatGuid,
        int PersonId,
        Guid PersonGuid,
        decimal Price,
        DateTime RowControl,
        int? QueueMessageId);

}
