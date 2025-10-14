using LogicApi.Model.Request.Order;
using LogicApi.Model.Response.Order;
using LogicApi.Model.Response.Order.Common;

namespace LogicApi.BusinessLogic.OrderHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class VerifyOrderValuesHandler(
    ILogger<VerifyOrderValuesHandler> logger,
    IPluginFactory pluginFactory) : OrderBase<VerifyOrderValuesRequest, VerifyOrderValuesResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<VerifyOrderValuesResponse> Handle(VerifyOrderValuesRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.VerifyOrderValues, request, async () =>
        {
            var routeInformation = await GetRouteInformationCacheAsync(request.RouteGuid).ConfigureAwait(false);
            await SetCooperativeAsync(routeInformation.CooperativeId).ConfigureAwait(false);
            //Buscamos en la base de datos los Ids
            var seatPersonGuids = request.SeatPeople.Select(t => t.PersonGuid);
            var persons = await AuthenticationUnitOfWork.PersonRepository.GetGenericAsync(
                select => new
                {
                    select.Guid,
                    select.Id,
                    select.Name,
                    select.LastName,
                    select.DocumentNumber
                },
                where => seatPersonGuids.Contains(where.Guid)
            ).ConfigureAwait(false);
            //Verifica las personas registradas
            if (!seatPersonGuids.ContainsAll(persons.Select(select => select.Guid), out var personNotFound))
                throw new CustomException((int)MessagesCodesError.PersonInformationNotFound, $"No se encontraron los ids de Personas: '{personNotFound.Join()}'");
            //Obtiene los asientos del request
            var seatGuids = request.SeatPeople.Select(t => t.SeatGuid);
            var seats = await CoreUnitOfWork.ReserveSeatRepository.GetGenericAsync(
                select => new
                {
                    select.CooperativeRouteId,
                    select.Id,
                    seatGuid = select.Guid,
                    select.UserId,
                    select.State,
                    select.SeatIdentifier
                },
                where => seatGuids.Contains(where.Guid)
            ).ConfigureAwait(false);
            //Valida que exista todos los ids de asientos del request
            if (!seatGuids.ContainsAll(seats.Select(t => t.seatGuid), out var listSeatsNotMatch))
                throw new CustomException((int)MessagesCodesError.OrderModelError, $"No se encontraron los asientos reservados con Ids: '{listSeatsNotMatch.Join()}'.");
            //Valida que los asientos esten reservados
            if (seats.Any(where => where.State != SeatState.Reserved, out var seatsNotReserved))
                throw new CustomException((int)MessagesCodesError.OrderModelError, $"Los asientos no estan reservados: '{seatsNotReserved.Join()}'.");
            //Valida que el asiento este reservado por el usuario
            if (seats.Any(where => where.UserId != UserId, out var seatWithOtherUser))
                throw new CustomException((int)MessagesCodesError.SeatReserved, $"El asiento no esta reservado por el usuario: '{seatWithOtherUser.Join()}'.");
            //Realiza un Join de la data
            var seatPersonInformation = seats.Join(
                request.SeatPeople,
                seat => seat.seatGuid,
                seatPerson => seatPerson.SeatGuid,
                (seat, seatPerson) => new
                {
                    seat.SeatIdentifier,
                    seatPerson.PersonGuid
                }
            ).Join(persons,
            seatPerson => seatPerson.PersonGuid,
            person => person.Guid,
            (seatPerson, person) => new SeatPersonInformation
            {
                PersonIdentifier = person.DocumentNumber,
                PersonName = $"{person.Name} {person.LastName}",
                SeatType = "Bus/Cama",
                SeatIdentifier = seatPerson.SeatIdentifier,
            }
            );
            //Calcula los valores
            var values = new Dictionary<string, decimal>{
                  {"Subtotal", request.SeatPeople.Sum(t => t.Price)},
                  {"Iva", request.SeatPeople.Sum(t => t.Price)*0.12m},
                  {"Gastos Varios", 0.50m},
              };
            //Retorna la respuesta
            var transportPointIds = new List<int> { routeInformation.OriginTransportPointId, routeInformation.DestinationTransportPointId };
            var dictionaryTransportPoint = (await AdministrationUnitOfWork.CooperativeTransportPointRepository.GetGenericAsync(
                select => new
                {
                    select.Id,
                    TransportPoitName = select.TransportPoint.Name,
                    ProvinceName = select.TransportPoint.Province.Name
                },
                where => transportPointIds.Contains(where.Id)
            ).ConfigureAwait(false))
            .ToDictionary(key => key.Id, value => value);
            return new VerifyOrderValuesResponse
            {
                Orders = [
                    new GenerateOrderItem{
                          CompanyName = Cooperative.Name,
                          DateTimeDestination = routeInformation.DateTimeRouteTimeArrival,
                          DateTimeOrigin = routeInformation.DateTimeRouteTime,
                          DestinationProvinceName = dictionaryTransportPoint[routeInformation.DestinationTransportPointId].ProvinceName,
                          DestinationTransportPointName = dictionaryTransportPoint[routeInformation.DestinationTransportPointId].TransportPoitName,
                          OriginProvinceName = dictionaryTransportPoint[routeInformation.OriginTransportPointId].ProvinceName,
                          OriginTransportPointName = dictionaryTransportPoint[routeInformation.OriginTransportPointId].TransportPoitName,
                          SeatPersonInformation =seatPersonInformation
                      },
                  ],
                Values = values
            };
            //Retorna la respuesta
        }, [UnitOfWorkType.Authentication, UnitOfWorkType.Administration, UnitOfWorkType.Core]);

}
