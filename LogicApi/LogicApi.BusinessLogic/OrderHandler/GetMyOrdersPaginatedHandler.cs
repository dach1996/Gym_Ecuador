using LogicApi.Model.Request.Order;
using LogicApi.Model.Response.Order;
using LogicApi.Model.Response.Order.Common;

namespace LogicApi.BusinessLogic.OrderHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetMyOrdersPaginatedHandler(
    ILogger<GetMyOrdersPaginatedHandler> logger,
    IPluginFactory pluginFactory) : OrderBase<GetMyOrdersPaginatedRequest, GetMyOrdersPaginatedResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetMyOrdersPaginatedResponse> Handle(GetMyOrdersPaginatedRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetMyOrdersPaginated, request, async () =>
        {
            //Obtiene solo los viajes realizados
            var ordersRepository = await CoreUnitOfWork.OrderRepository.GetPaginatorGenericAsync(
                request.PageSize,
                request.PageNumber,
                select => new
                {
                    OrderGuid = select.Guid,
                    OriginDateTime = select.CooperativeRoute.DateTimeRouteTime,
                    DestinationDateTime = select.CooperativeRoute.DateTimeRouteTimeArrival,
                    OriginProvinceName = select.CooperativeRoute.OriginTransportPoint.TransportPoint.Province.Name,
                    DestinationProvinceName = select.CooperativeRoute.DestinationTransportPoint.TransportPoint.Province.Name,
                    OriginTransportPointName = select.CooperativeRoute.OriginTransportPoint.TransportPoint.Name,
                    DestinationTransportPointName = select.CooperativeRoute.DestinationTransportPoint.TransportPoint.Name,
                    select.State,
                },
                where => where.UserId == UserId,
                orderBy => orderBy.CooperativeRoute.DateTimeRouteTime,
                request.SortableType == EnumCommon.SortableType.Asc ? OrderByType.Asc : OrderByType.Desc
            ).ConfigureAwait(false);
            //Retorna el Recorrido
            return new GetMyOrdersPaginatedResponse(ordersRepository.TotalItems, ordersRepository.Items.Select(
                select => new OrderItem
                {
                    OrderGuid = select.OrderGuid,
                    OriginDateTime = select.OriginDateTime,
                    DestinationDateTime = select.DestinationDateTime,
                    OriginProvinceName = select.OriginProvinceName,
                    DestinationProvinceName = select.DestinationProvinceName,
                    OriginTransportPointName = select.OriginTransportPointName,
                    DestinationTransportPointName = select.DestinationTransportPointName,
                    State = GetOrderViewState(select.State, select.OriginDateTime)
                }
            ));
        }, UnitOfWorkType.Core);


}
