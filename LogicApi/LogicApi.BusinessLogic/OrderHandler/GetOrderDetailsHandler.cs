using LogicApi.Model.Request.Order;
using LogicApi.Model.Response.Order;
using LogicApi.Model.Response.Order.Common;

namespace LogicApi.BusinessLogic.OrderHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetOrderDetailsHandler(
    ILogger<GetOrderDetailsHandler> logger,
    IPluginFactory pluginFactory)
    : OrderBase<GetOrderDetailsRequest, GetOrderDetailsResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetOrderDetailsResponse> Handle(GetOrderDetailsRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetOrderDetails, request, async () =>
        {
            //Obtiene la Orden
            var order = (await CoreUnitOfWork.OrderRepository
                 .GetFirstOrDefaultGenericAsync(
                    select => new
                    {
                        select.Guid,
                        select.CooperativeRoute.CooperativeId,
                        select.CooperativeRoute.DateTimeRouteTime,
                        select.CooperativeRoute.DateTimeRouteTimeArrival,
                        ProvinceNameDestination = select.CooperativeRoute.DestinationTransportPoint.TransportPoint.Province.Name,
                        TransportPointNameDestination = select.CooperativeRoute.DestinationTransportPoint.TransportPoint.Name,
                        ProvinceNameOrigin = select.CooperativeRoute.OriginTransportPoint.TransportPoint.Province.Name,
                        TransportPointNameOrigin = select.CooperativeRoute.OriginTransportPoint.TransportPoint.Name,
                        select.State,
                        SeatInformation = select.OrderSeatPeople
                            .Select(seat => new
                            {
                                seat.ReserveSeat.SeatIdentifier,
                                seat.Person,
                                seat.Price
                            })
                    },
                     where => where.Id == request.OrderId).ConfigureAwait(false))
                 ?? throw new CustomException((int)MessagesCodesError.OrderNotFound, $"Orden con Id: '{request.OrderId}' no fué encontrada");
            await SetCooperativeAsync(order.CooperativeId).ConfigureAwait(false);
            //Verifica que los ids de estación correspondan a la cooperativa
            //var stationCooperatives = Cooperative.GetPlaceAndStationByCooperativeIds(order.DestinationCooperativeStationId, order.OriginCooperativePlaceStationId);  //Buscamos en la base de datos los Ids
            var seatPersonInformation = order.SeatInformation.Select(t => new SeatPersonInformation
            {
                PersonIdentifier = t.Person.DocumentNumber,
                PersonName = $"{t.Person.Name} {t.Person.LastName}",
                SeatType = "Bus/Cama",
                SeatIdentifier = t.SeatIdentifier,
            });

            var values = new Dictionary<string, decimal>{
                {"Subtotal", order.SeatInformation.Sum(t => t.Price)},
                {"Iva", order.SeatInformation.Sum(t => t.Price)*0.12m},
                {"Gastos Varios", 0.50m},
            };
            return new GetOrderDetailsResponse(new()
            {
                OrderGuid = order.Guid,
                CompanyName = Cooperative.Name,
                DateTimeDestination = order.DateTimeRouteTimeArrival,
                DateTimeOrigin = order.DateTimeRouteTime,
                DestinationProvinceName = order.ProvinceNameDestination,
                DestinationTransportPointName = order.TransportPointNameDestination,
                OriginProvinceName = order.ProvinceNameOrigin,
                OriginTransportPointName = order.TransportPointNameOrigin,
                SeatPersonInformation = seatPersonInformation,
                State = GetOrderViewState(order.State, order.DateTimeRouteTime),
            }, values);
            //Retorna la respuesta
        }, [UnitOfWorkType.Authentication, UnitOfWorkType.Core]);

}
