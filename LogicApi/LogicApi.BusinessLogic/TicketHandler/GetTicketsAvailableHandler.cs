using Common.Cooperative;
using Common.Cooperative.Models.Request;
using LogicApi.Model.Common;
using LogicApi.Model.Request.Ticket;
using LogicApi.Model.Response.Ticket;
using PersistenceDb.Models.Core;
namespace LogicApi.BusinessLogic.TicketHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetTicketsAvailableHandler(
    ILogger<GetTicketsAvailableHandler> logger,
    IPluginFactory pluginFactory) : TicketBase<GetTicketsAvailableRequest, GetTicketsAvailableResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async override Task<GetTicketsAvailableResponse> Handle(GetTicketsAvailableRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetTicketsAvailable, request, async () =>
        {
            //Obtiene las cooperativas y asigna a la búsqueda en caso de no haber nada
            var coopertiveData = await GetCooperativeDataAsync().ConfigureAwait(false);
            var placeInformation = await GetPlaceInformationAsync().ConfigureAwait(false);

            //Obtiene el código de la provincia de origen
            var originProvinceId = placeInformation.GetValueOrException(request.PlaceCodeOrigin, new CustomException((int)MessagesCodesError.PlaceNotFound, $"No se encontró el código: '{request.PlaceCodeOrigin}' de origen"));
            var destinationProvinceId = placeInformation.GetValueOrException(request.PlaceCodeDestination, new CustomException((int)MessagesCodesError.PlaceNotFound, $"No se encontró el código: '{request.PlaceCodeDestination}' de destino"));
            //Obtiene la cooperativa que tengan puntos de partida y destino en las provincias permitidas
            var cooperativeProvince = coopertiveData.GetCooperativeDataByAllowedProvinceId(originProvinceId.ProvinceId, destinationProvinceId.ProvinceId);
            if (cooperativeProvince.IsNullOrEmpty())
                throw new CustomException((int)MessagesCodesError.RouteDontAvailable, "No se encontraron rutas disponibles para los puntos de partida y destino");
            var routesDataBaseTask = CoreUnitOfWork.CooperativeRouteRepository.GetGenericAsync(
                select => new
                {
                    select.Guid,
                    select.CooperativeId,
                    select.TicketIdentifier,
                    select.CooperativeBusId
                },
              where => where.DateTimeRoute == request.DateTimeFrom.Date
            );
            var implementationCodes = coopertiveData.GetImplementationCodeByCooperativeIds(cooperativeProvince.Select(select => select.Id));
            var implementations = PluginFactory.GetPlugins<ICooperativeServices>(implementationCodes);
            //Realiza un join para encontrar la data y la implementación
            var cooperativeDataImplementations = coopertiveData.CooperativeItems.Join(
                implementations,
                coop => coop.Code,
                i => i.Key,
                (coop, imp) => new
                {
                    Implementation = imp.Value,
                    CooperativeData = coop
                }
            );
            List<Task<IEnumerable<TicketAvailable>>> task = [];
            foreach (var item in cooperativeDataImplementations)
                task.Add(GetListTicketAvailableAsync(
                    item.CooperativeData,
                    item.Implementation,
                    request,
                    new TicketPartialInformation
                    {
                        OriginProvinceName = placeInformation[request.PlaceCodeOrigin].ProvinceName,
                        DestinationProvinceName = placeInformation[request.PlaceCodeDestination].ProvinceName
                    }));
            //Crea una clase para llamar a las tareas múltiples 
            var listTickets = await Task.WhenAll(task).ConfigureAwait(false);
            var ticketRouteServices = listTickets?.SelectMany(ticket => ticket).Where(where => where is not null).ToArray();
            if (ticketRouteServices.GroupBy(group => new { group.Cooperative.CooperativeId, group.Identifier }).Any(where => where.Count() > 1, out var routesWithMoreThanOneTicket))
            {
                foreach (var item in routesWithMoreThanOneTicket.Select(select => select.Key))
                    Logger.LogWarning("Se encontraron Identificadores de Ticket de la Cooperativa: {@CooperativeId} con el identificador: {@Identifier} con más de una ruta", item.CooperativeId, item.Identifier);
                ticketRouteServices = [.. ticketRouteServices.Except(routesWithMoreThanOneTicket.SelectMany(select => select))];
            }
            var routesDataBase = await routesDataBaseTask.ConfigureAwait(false);
            Logger.LogDebug("RoutesDataBase: {@RoutesDataBase}", routesDataBase);
            Logger.LogDebug("TicketRouteServices: {@TicketRouteServices}", ticketRouteServices);
            var joinTicketsRoutes = ticketRouteServices.GroupJoin(
                routesDataBase,
                ticket => (ticket.Identifier, ticket.Cooperative.CooperativeId),
                route => (route.TicketIdentifier, route.CooperativeId),
                (ticket, route) => new
                {
                    Ticket = ticket,
                    RouteGuid = route.FirstOrDefault()?.Guid
                }).ToList();

            if (joinTicketsRoutes.Any(where => where.RouteGuid is null, out var routerWithoutDataBase))
            {
                Logger.LogDebug("Nuevos Tickets a Registrar: {@RouterWithoutDataBase}", routerWithoutDataBase);
                var finalRoutes = new List<CooperativeRoute>();
                foreach (var joinTicketRoute in routerWithoutDataBase.Select(select => select.Ticket))
                    finalRoutes.Add(await CoreUnitOfWork.CooperativeRouteRepository.TryAddOrGetFirstAsync(
                       new CooperativeRoute
                       {
                           Guid = Guid.NewGuid(),
                           DateTimeRegister = Now,
                           CooperativeId = joinTicketRoute.Cooperative.CooperativeId,
                           CooperativeBusId = joinTicketRoute.BusCooperativeId,
                           DateTimeRoute = request.DateTimeFrom,
                           TicketIdentifier = joinTicketRoute.Identifier,
                           OriginTransportPointId = joinTicketRoute.OriginTransportPointId,
                           DestinationTransportPointId = joinTicketRoute.DestinationTransportPointId,
                           DateTimeRouteTime = joinTicketRoute.Schedule.OriginDateTime,
                           DateTimeRouteTimeArrival = joinTicketRoute.Schedule.DestinationDateTime
                       },
                            where => where.TicketIdentifier == joinTicketRoute.Identifier
                            && where.CooperativeId == joinTicketRoute.Cooperative.CooperativeId
                            && where.DateTimeRoute == request.DateTimeFrom.Date
                            && where.CooperativeBusId == joinTicketRoute.BusCooperativeId
                        ).ConfigureAwait(false));
                routesDataBase = [.. routesDataBase.Concat(finalRoutes.Select(select => new { select.Guid, select.CooperativeId, select.TicketIdentifier, select.CooperativeBusId }))];
                joinTicketsRoutes = [.. ticketRouteServices.GroupJoin(
                 routesDataBase,
                 ticket => (ticket.Identifier, ticket.Cooperative.CooperativeId),
                 route => (route.TicketIdentifier, route.CooperativeId),
                 (ticket, route) => new
                 {
                     Ticket = ticket,
                     RouteGuid = route.FirstOrDefault()?.Guid
                 })];
                //Espera las tareas aplicadas
            }
            return new GetTicketsAvailableResponse(joinTicketsRoutes.Select(select =>
            {
                select.Ticket.RouteGuid = select.RouteGuid.Value;
                return select.Ticket;
            }));
        }, UnitOfWorkType.Core);





    /// <summary>
    /// Obtiene los Tickes Disponibles
    /// </summary>
    /// <param name="cooperativeInformation"></param>
    /// <param name="implementation"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    private async Task<IEnumerable<TicketAvailable>> GetListTicketAvailableAsync(
        CooperativeItemData cooperativeInformation,
        ICooperativeServices implementation,
        GetTicketsAvailableRequest request,
        TicketPartialInformation ticketPartialInformation)
    {
        try
        {
            //Realiza la consulta a la implementación de la cooperativa
            var getAvailableTripsResponse = await implementation.GetAvailableTripsAsync(new GetAvailableTripsRequest
            {
                DateTimeFrom = request.DateTimeFrom,
                DateTimeTo = request.DateTimeTo,
                PlaceCodeDestination = cooperativeInformation.GetTransportPointNameByCodeCooperativeStation(request.PlaceCodeDestination),
                PlaceCodeOrigin = cooperativeInformation.GetTransportPointNameByCodeCooperativeStation(request.PlaceCodeOrigin),
                MaxPrice = request.MinPrice,
                MinPrice = request.MinPrice,
                BusTypes = request.BusTypes
            }).ConfigureAwait(false);
            var tickets = getAvailableTripsResponse.TicketAvailables
                .RemoveWhere(
                    where => !cooperativeInformation.GetTransportPointIdByPlaceCode(where.DestinationStationCode).HasValue
                    || !cooperativeInformation.GetTransportPointIdByPlaceCode(where.OriginStationCode).HasValue,
                    out var ticketsWithoutTransportPoint
                );
            if (!ticketsWithoutTransportPoint.IsNullOrEmpty())
                Logger.LogWarning("Se encontraron Pasajes sin puntos de transporte disponibles para la Cooperativa: {@CooperativeId} - {@TicketAvailables}", cooperativeInformation.Id, ticketsWithoutTransportPoint);

            return [.. tickets.Select(
                select => new TicketAvailable
                {
                    Identifier = select.Identifier,
                    BusCooperativeId = cooperativeInformation.GetBusInformation(select.BusCode).CooperativeBusId,
                    Cooperative = new CooperativeInformation
                    {
                        CooperativeId = cooperativeInformation.Id,
                        Name = cooperativeInformation.Name,
                        UrlImage = cooperativeInformation.BusImageUrl
                    },
                    ApproximatePrice = select.ApproximatePrice,
                    IsBest = false,
                    OriginTransportPointId = cooperativeInformation.GetTransportPointIdByPlaceCode(select.OriginStationCode).Value,
                    DestinationTransportPointId = cooperativeInformation.GetTransportPointIdByPlaceCode(select.DestinationStationCode).Value,
                    Schedule = new Schedule
                    {
                        DestinationDateTime = select.Schedule.DestinationDateTime,
                        OriginDateTime = select.Schedule.OriginDateTime,
                        DestinationTransportPointName = cooperativeInformation.GetTransportPointNameByCodeCooperativeStation(select.DestinationStationCode),
                        OriginTransportPointName = cooperativeInformation.GetTransportPointNameByCodeCooperativeStation(select.OriginStationCode),
                        ProvinceOriginName = ticketPartialInformation.OriginProvinceName,
                        ProvinceDestinationName = ticketPartialInformation.DestinationProvinceName,
                    },
                    BusType = (EnumCommon.BusType)select.BusType,
                    SeatAvailable = select.SeatAvailable,
                }
            )];
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "No se pudo obtener los Pasajes disponisbles de la Cooperativa: {@Implementation}", implementation);
            return [];
        }
    }
}