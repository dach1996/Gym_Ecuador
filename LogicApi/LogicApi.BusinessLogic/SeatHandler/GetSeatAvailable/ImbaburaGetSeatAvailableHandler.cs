using LogicApi.Abstractions.Interfaces.Seat;
using LogicApi.Model.Request.Seat;
using LogicApi.Model.Response.Seat;
namespace LogicApi.BusinessLogic.SeatHandler.GetSeatAvailable;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class ImbaburaGetSeatAvailableHandler(
    ILogger<ImbaburaGetSeatAvailableHandler> logger,
    IPluginFactory pluginFactory) : GetSeatAvailableHandler(
        logger,
        pluginFactory), IGetSeatAvailableHandler
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<GetSeatAvailableResponse> Handle(GetSeatAvailableRequest request)
        => await ExecuteResponseAsync(request).ConfigureAwait(false);


}