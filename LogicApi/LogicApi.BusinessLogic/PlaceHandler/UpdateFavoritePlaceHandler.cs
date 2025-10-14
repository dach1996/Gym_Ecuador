using LogicApi.Model.Request.Place;
using LogicApi.Model.Response.Place;
using PersistenceDb.Models.Administration;

namespace LogicApi.BusinessLogic.PlaceHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateFavoritePlaceHandler(
    ILogger<UpdateFavoritePlaceHandler> logger,
    IPluginFactory pluginFactory) : PlaceBase<UpdateFavoritePlaceRequest, UpdateFavoritePlaceResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<UpdateFavoritePlaceResponse> Handle(UpdateFavoritePlaceRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.UpdateFavoritePlace, request, async () =>
        {
            //Busca el lugar por el código
            var placeId = (await AdministrationUnitOfWork.PlaceRepository
            .GetFirstOrDefaultGenericAsync<int?>(
                select => select.Id,
                where => where.Code == request.PlaceCode).ConfigureAwait(false))
                ?? throw new CustomException((int)MessagesCodesError.SystemError, $"No se encontró el Lugar con código: '{request.PlaceCode}'");
            //Busca si existe un registro asociado al usuario caso contrario lo crea
            var placeUser = (await AdministrationUnitOfWork.PlaceUserRepository
                .GetByFirstOrDefaultAsync(p => p.UserId == UserId && p.PlaceId == placeId).ConfigureAwait(false))
                ?? await AdministrationUnitOfWork.PlaceUserRepository.AddAsync(new PlaceUser
                {
                    PlaceId = placeId,
                    UserId = UserId
                }).ConfigureAwait(false);
            //Verifica cuál ambito cambiar
            if (request.PlaceFavoriteType == PlaceFavoriteType.Origin)
                placeUser.IsFavoriteOrigin = !placeUser.IsFavoriteOrigin;
            if (request.PlaceFavoriteType == PlaceFavoriteType.Destination)
                placeUser.IsFavoriteDestination = !placeUser.IsFavoriteDestination;
            await AdministrationUnitOfWork.PlaceUserRepository.UpdateAsync(placeUser).ConfigureAwait(false);
            var getMyFavoritePlaceResponse = await Mediator.Send(new GetMyFavoritePlaceRequest(request.ContextRequest)).ConfigureAwait(false);
            //Retorna la respuesta
            return new UpdateFavoritePlaceResponse
            {
                FavoritePlaces = request.PlaceFavoriteType == PlaceFavoriteType.Origin 
                    ? getMyFavoritePlaceResponse.FavoriteOriginPlaces 
                    : getMyFavoritePlaceResponse.FavoriteDestinationPlaces,
                UserMessage = getMyFavoritePlaceResponse.UserMessage,
                ShowMessage = getMyFavoritePlaceResponse.ShowMessage
            };
        }, [UnitOfWorkType.Administration]);
}
