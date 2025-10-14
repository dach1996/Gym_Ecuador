using LogicApi.Model.Request.Place;
using LogicApi.Model.Response.Place;
namespace LogicApi.BusinessLogic.PlaceHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetPlacesPaginatedHandler(
    ILogger<GetPlacesPaginatedHandler> logger,
    IPluginFactory pluginFactory) : PlaceBase<GetPlacesPaginatedRequest, GetPlacesPaginatedResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary> 
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetPlacesPaginatedResponse> Handle(GetPlacesPaginatedRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetPlacesPaginated, request, async () =>
        {
            var placeItems = await AdministratorCache.TryGetOrSetAsync(CacheCodes.PLACES, async () =>
                 await AdministrationUnitOfWork.PlaceRepository.GetGenericAsync(
                    select => new PlaceItem
                    {
                        Code = select.Code,
                        Name = select.Name,
                        ShortName = select.ShortName
                    },
                where => where.State
            ));
            var placePaginator = (!request.PropertySearch.IsNullOrEmpty() ? placeItems
            .Where(where => where.Name.Contains(request.PropertySearch, StringComparison.OrdinalIgnoreCase)) : placeItems)
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize);
            //Retorna la respuesta
            return new GetPlacesPaginatedResponse(placeItems.Count, placePaginator);
        }, [UnitOfWorkType.Administration]);
}
