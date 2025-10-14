using LogicApi.Model.Request.Order;
using LogicApi.Model.Response.Order;

namespace LogicApi.BusinessLogic.OrderHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetInitialMyOrdersHandler(
    ILogger<GetInitialMyOrdersHandler> logger,
    IPluginFactory pluginFactory) : OrderBase<GetInitialMyOrdersRequest, GetInitialMyOrdersResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetInitialMyOrdersResponse> Handle(GetInitialMyOrdersRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetInitialMyOrdersPaginated, request, async () =>
        {
            //Obtiene solo los viajes realizados
            var ordersActive = await CoreUnitOfWork.OrderRepository.GetGenericAsync(
               select => new
               {
                   OrderGuid = select.Guid,
                   OriginDateTime = select.CooperativeRoute.DateTimeRouteTime,
                   DestinationDateTime = select.CooperativeRoute.DateTimeRouteTimeArrival,
                   OriginProvinceName = select.CooperativeRoute.OriginTransportPoint.TransportPoint.Province.Name,
                   DestinationProvinceName = select.CooperativeRoute.DestinationTransportPoint.TransportPoint.Province.Name,
                   OriginTransportPointName = select.CooperativeRoute.OriginTransportPoint.TransportPoint.Name,
                   DestinationTransportPointName = select.CooperativeRoute.DestinationTransportPoint.TransportPoint.Name,
                   DateTimeExpired = select.DateTimeExpiration,
                   select.State,
               },
               where => where.UserId == UserId
                && new[] { OrderState.Paid, OrderState.Created }.Contains(where.State),
               orderBy => orderBy.CooperativeRoute.DateTimeRouteTime,
               OrderByType.Desc
           ).ConfigureAwait(false);
            return new GetInitialMyOrdersResponse(ordersActive.Select(select => new OrderItemActive
            {
                OrderGuid = select.OrderGuid,
                OriginDateTime = select.OriginDateTime,
                DestinationDateTime = select.DestinationDateTime,
                OriginProvinceName = select.OriginProvinceName,
                DestinationProvinceName = select.DestinationProvinceName,
                OriginTransportPointName = select.OriginTransportPointName,
                DestinationTransportPointName = select.DestinationTransportPointName,
                State = GetOrderViewState(select.State, select.OriginDateTime),
                DateTimeExpired = select.DateTimeExpired
            }));
        }, UnitOfWorkType.Core);
}
