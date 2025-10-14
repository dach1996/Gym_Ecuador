using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Request.Place;
using LogicApi.Model.Response;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class PlaceController : SecurityControllerBase
{

    /// <summary>
    /// Constructor
    /// </summary>
    public PlaceController(
        IUserMessages userMessages,
        ILogger<PlaceController> logger,
        IMediator mediator
        ) : base(
            userMessages,
            logger,
            mediator)
    {
    }


    #region Methods Controller

    /// <summary>
    /// Actualizar Lugares Favoritos
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("Favorite")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<HandlerResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse<HandlerResponse>))]
    public async Task<IActionResult> UpdateFavoritePlace([FromBody] UpdateFavoritePlaceRequest request)
          => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene los lugares paginados
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("GetPlacesPaginated")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<HandlerResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse<HandlerResponse>))]
    public async Task<IActionResult> GetPlacesPaginated([FromQuery] GetPlacesPaginatedRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene los lugares favoritos
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("GetMyFavoritePlaces")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<HandlerResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse<HandlerResponse>))]
    public async Task<IActionResult> GetMyFavoritePlace([FromQuery] GetMyFavoritePlaceRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}