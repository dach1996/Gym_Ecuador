using LogicApi.Model.Request.Place;
using LogicApi.Model.Response.Place;
namespace LogicApi.BusinessLogic.PlaceHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetMyFavoritePlaceHandler(
    ILogger<GetMyFavoritePlaceHandler> logger,
    IPluginFactory pluginFactory) : PlaceBase<GetMyFavoritePlaceRequest, GetMyFavoritePlaceResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary> 
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetMyFavoritePlaceResponse> Handle(GetMyFavoritePlaceRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetMyFavoritePlace, request, async () =>
        {
            var favoritePlaces = (await AdministrationUnitOfWork.PlaceUserRepository.GetGenericAsync(
                select => new
                {
                    select.Place.Code,
                    select.Place.ShortName,
                    select.IsFavoriteOrigin,
                    select.IsFavoriteDestination,
                },
                where => where.UserId == UserId
            )).Where(where => where.IsFavoriteOrigin || where.IsFavoriteDestination);
            var originFavoritePlaces = favoritePlaces.Where(where => where.IsFavoriteOrigin)
                .ToDictionary(select => select.Code, select => select.ShortName);
            var destinationFavoritePlaces = favoritePlaces.Where(where => where.IsFavoriteDestination)
                .ToDictionary(select => select.Code, select => select.ShortName);
            //Retorna la respuesta
            return new GetMyFavoritePlaceResponse
            {
                FavoriteOriginPlaces = originFavoritePlaces,
                FavoriteDestinationPlaces = destinationFavoritePlaces
            };
        }, [UnitOfWorkType.Administration]);
}
