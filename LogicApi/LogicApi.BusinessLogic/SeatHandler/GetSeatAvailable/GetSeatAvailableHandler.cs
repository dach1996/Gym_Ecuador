using Common.Cooperative;
using Common.Cooperative.Models.Response;
using Common.Queue.Model.Template;
using Common.Utils;
using LogicApi.Abstractions.Interfaces.Seat;
using LogicApi.Model.Common;
using LogicApi.Model.Request.Seat;
using LogicApi.Model.Response.Seat;
using PersistenceDb.Models.Core;

namespace LogicApi.BusinessLogic.SeatHandler.GetSeatAvailable;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class GetSeatAvailableHandler(
    ILogger<GetSeatAvailableHandler> logger,
    IPluginFactory pluginFactory) : SeatBase<GetSeatAvailableRequest, GetSeatAvailableResponse>(
        logger,
        pluginFactory)
{
    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>

    public override async Task<GetSeatAvailableResponse> Handle(GetSeatAvailableRequest request, CancellationToken cancellationToken)
    => await ExecuteHandlerAsync(OperationApiName.GetSeatAvailable, request, async () =>
    {
        var lastRouteViewByUser = await AdministratorCache.TryGetOrSetAsync(
            CacheCodes.LastRouteViewByUser(UserId),
            () => Task.FromResult(request.RouteGuid),
            slidingExpiration: true).ConfigureAwait(false);
        if (lastRouteViewByUser != request.RouteGuid)
            //Envía un queue para eliminar reserva de otros asientos
            _ = SendQueueMessageAsync(new VoidSeatReservationQueueTemplate(UserId, request.RouteGuid)).ConfigureAwait(false);
        //Obtiene le Id de la cooperativa por Guid de Ruta
        //Obtiene las implementaciones de cooperativas
        var cooperativeImplementation = await GetCooperativeImplementationByIdAsync(request.RouteGuid).ConfigureAwait(false);
        //Ejecuta la implementación seleccionada
        return await PluginFactory.GetPlugin<IGetSeatAvailableHandler>(cooperativeImplementation).Handle(request).ConfigureAwait(false);
    }, [UnitOfWorkType.Core, UnitOfWorkType.Administration]);


    /// <summary>
    /// Obtiene la implementación por Id de Cooperativa
    /// </summary>
    /// <param name="cooperativeId"></param>
    /// <returns></returns>
    private async Task<string> GetCooperativeImplementationByIdAsync(Guid routeGuid)
    {
        var routeInformation = await GetRouteInformationCacheAsync(routeGuid).ConfigureAwait(false);
        return (await GetCooperativesImplementationAsync().ConfigureAwait(false)).FirstOrDefault(where => where.Id == routeInformation.CooperativeId)
                 ?.CooperativeImplementationName
                 ?? throw new CustomException((int)MessagesCodesError.ImplementationDontFound, $"No se encontró la implementación de Cooperativa con Id: {routeInformation.CooperativeId}");
    }

    /// <summary>
    /// Obtiene las cooperativas y sus implementaciones
    /// </summary>
    /// <returns></returns>
    private async Task<IEnumerable<CooperativeImplementation>> GetCooperativesImplementationAsync()
        => await AdministratorCache.TryGetOrSetAsync(CacheCodes.COOPERATIVES_IMPLEMENTATION, async () =>
            {
                var dictionaryImplementations = Util.GetDictionaryEnums<CooperativeImplementationName>();
                var cooperativeImplementation = await AdministrationUnitOfWork.CooperativeRepository
                    .GetGenericAsync(
                                select => new CooperativeImplementation
                                {
                                    Id = select.Id,
                                    Code = select.Code,
                                },
                                where => where.State
                            ).ConfigureAwait(false);
                return cooperativeImplementation.Join(
                    dictionaryImplementations,
                    ci => ci.Code,
                    di => di.Value,
                    (ci, di) => new CooperativeImplementation
                    {
                        Code = ci.Code,
                        Id = ci.Id,
                        CooperativeImplementationName = $"{di.Key}"
                    });
            }).ConfigureAwait(false);

    /// <summary>
    /// Ejecuta respuesta 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected async Task<GetSeatAvailableResponse> ExecuteResponseAsync(GetSeatAvailableRequest request)
    => await ExecuteHandlerAsync(OperationApiName.GetSeatAvailable, request, async () =>
    {
        var routeInformation = await GetRouteInformationCacheAsync(request.RouteGuid).ConfigureAwait(false);
        await SetCooperativeAsync(routeInformation.CooperativeId).ConfigureAwait(false);
        //Obtiene los asientos de la cooperativa
        var busServiceInformation = await GetBusInformationByCooperativeServiceAsync(routeInformation.CooperativeId, routeInformation.RouteIdentifier).ConfigureAwait(false);
        var busDataBaseInformation = Cooperative.GetBusInformation(busServiceInformation.BusIdentifier);
        var price = 10.00m; //TODO: Obtener el precio real de los asientos
        var busDiagramSpacesDataBase = busDataBaseInformation.GetCooperativeFloors()
            .Select((select, index) => new FloorInformation
            {
                FloorGuid = select.CooperativeFloorGuid,
                FloorLabel = $"Piso {index + 1}",
                Rows = select.GetFloorSpaces()
                    .Select(rowOriginal => new RowInformation
                    {
                        Spaces = rowOriginal
                        .Select(space =>
                           new BusSpaceInformation(
                            identifier: space.SeatIdentifier,
                            busSpaceType: space.BusSpaceType,
                            price: price))
                    })
            });

        //Obtiene los asientos y estados
        var seats = await GetBusCooperativeSeatsAsync(routeInformation, busServiceInformation, busDataBaseInformation).ConfigureAwait(false);
        //Recorre la lista de datos extraidos de la consulta para actualizar los asientos del diagrama
        var seatsKey = busDataBaseInformation.GetSeatsKey();
        var seatWithStateKey = seats.Select(select => $"{select.FloorCooperativeGuid}{select.SeatIdentifier}".ToLower());
        if (seatWithStateKey.Any(where => !seatsKey.Contains(where), out var seatWithState))
            Logger.LogWarning("Los asientos de consulta con código: {@Code} no fueron encontrados en el diagrama del Bus {@BusInformation}", seatWithState, busServiceInformation.BusIdentifier);
        return new GetSeatAvailableResponse(
            routeGuid: request.RouteGuid,
            services: busDataBaseInformation.Services,
            floors: busDiagramSpacesDataBase,
            seats: seats);
    }, [UnitOfWorkType.Administration, UnitOfWorkType.Core]);


    /// <summary>
    /// Obtiene asientos y estados
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cooperativeServicesImplementation"></param>
    /// <returns></returns>
    private async Task<IEnumerable<SeatNewStateModel>> GetBusCooperativeSeatsAsync(
        RouteInformationCacheModel routeInformation,
        GetBusSeatResponse getSeatsStateByCooperativeService,
        BusInformation busDataBaseInformation)
    {
        //Lista de asientos comprados en estación
        var seatsStateServiceInformation = getSeatsStateByCooperativeService.Seats.ToList();
        var busInformation = Cooperative.GetBusInformation(getSeatsStateByCooperativeService.BusIdentifier);
        //Busca los asientos registrados en base de datos.
        var seatsDataBase = await CoreUnitOfWork.ReserveSeatRepository.GetGenericAsync(
            select => new
            {
                select.Id,
                select.Guid,
                select.SeatIdentifier,
                select.FloorBusCooperativeId,
                select.State,
                select.UserId,
                select.DateTimeExpiration
            },
            where => where.CooperativeRouteId == routeInformation.RouteId
        ).ConfigureAwait(false);
        //Verifica los registros que ya han sido comprados en estación, no se encuentran en la base, y los almacen
        var seatPurchasedNotRegister = seatsStateServiceInformation
            .Where(where =>
                !seatsDataBase
                    .Exists(any => any.SeatIdentifier == where.SeatIdentifier &&
                        any.FloorBusCooperativeId == busInformation.GetFloorIdByIdentifier(where.FloorIdentifier))
                && where.State == EnumCommonCop.SeatState.Purchased)
            .Select(select => new { select.SeatIdentifier, select.FloorIdentifier });
        if (!seatPurchasedNotRegister.IsNullOrEmpty())
        {
            var newSeats = seatPurchasedNotRegister.Select(seat => new ReserveSeat
            {
                CooperativeRouteId = routeInformation.RouteId,
                SeatIdentifier = seat.SeatIdentifier,
                FloorBusCooperativeId = busInformation.GetFloorIdByIdentifier(seat.FloorIdentifier),
                DateTimeRegister = Now,
                RowControl = Now,
                Guid = Guid.NewGuid(),
                State = SeatState.Purchased,
                DateTimeExpiration = null,
                UserId = null,
            }).ToList();
            var countNewRegisters = await CoreUnitOfWork.ReserveSeatRepository.AddRangeIdentityAsync(
                newSeats).ConfigureAwait(false);
            Logger.LogWarning("Se registraron: '{@CountNewRegisters}' nuevos asientos comprados: '{@Identifiers}'", countNewRegisters,
                seatPurchasedNotRegister.Select(select => $"*Piso: '{select.FloorIdentifier}' Asiento: '{select.SeatIdentifier}'*").Join());
            //Agrega los asientos a la lista base de datos
            seatsDataBase.AddRange(newSeats.Select(
                select => new
                {
                    select.Id,
                    select.Guid,
                    select.SeatIdentifier,
                    select.FloorBusCooperativeId,
                    select.State,
                    select.UserId,
                    select.DateTimeExpiration
                }));
        }
        //Valida los registros de base de datos que sean diferentes de comprados y que la consulta en la estación retorna comprados
        var seatsWithOtherState = seatsDataBase
            .Where(seatDataBase => seatsStateServiceInformation
                .Exists(any =>
                    seatDataBase.SeatIdentifier == any.SeatIdentifier
                    && seatDataBase.FloorBusCooperativeId == busInformation.GetFloorIdByIdentifier(any.FloorIdentifier)
                    && seatDataBase.State != SeatState.Purchased));
        //Si la lista está vacía 
        if (!seatsWithOtherState.IsNullOrEmpty())
        {
            var seatIdWithOtherState = seatsWithOtherState.Select(select => select.Id);
            var countUpdatedRegisters = await CoreUnitOfWork.ReserveSeatRepository.UpdateByAsync(
                update => new ReserveSeat
                {
                    State = SeatState.Purchased,
                    DateTimeRegister = Now,
                    UserId = null,
                    DateTimeExpiration = null
                },
                where => seatIdWithOtherState.Contains(where.Id)).ConfigureAwait(false);
            Logger.LogWarning("Se actualizaron: '{@CountNewRegisters}' asientos comprados: '{@Identifiers}'", countUpdatedRegisters,
              seatsWithOtherState.Select(select => $"*Piso: '{select.FloorBusCooperativeId}' Asiento: '{select.SeatIdentifier}'*").Join());
            seatsDataBase.AddRange(seatsWithOtherState.Select(
               select => new
               {
                   select.Id,
                   select.Guid,
                   select.SeatIdentifier,
                   select.FloorBusCooperativeId,
                   select.State,
                   select.UserId,
                   select.DateTimeExpiration
               }));
        }
        var userGuid = await GetUserInformationByUserIdAsync([.. seatsDataBase.Where(where => where.UserId.HasValue).Select(select => select.UserId.Value)]).ConfigureAwait(false);
        //Obtiene los asientos
        return seatsDataBase
            .Where(where => new[] { SeatState.Reserved, SeatState.Prepaid, SeatState.Purchased }.Contains(where.State))
            .Select(select => new SeatNewStateModel(
                    routeInformation.RouteGuid,
                    busDataBaseInformation.GetFloorGuidById(select.FloorBusCooperativeId),
                    select.Guid,
                     GetFinalState(select.State),
                    select.SeatIdentifier,
                     select.UserId.HasValue ? userGuid[select.UserId.Value].Guid : Guid.Empty));
    }


}
