using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response;
using LogicApi.Model.Request.Seat;
using LogicApi.Model.Response.Seat;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;
/// <summary>
/// Constructor
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class SeatController(
    IUserMessages userMessages,
    ILogger<SeatController> logger,
    IMediator mediator
        ) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{

    #region Methods Controller

    /// <summary>
    /// Registra un nuevo asiento al cliente en contexto
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("Reserve")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<ReserveSeatResponse>))]
    public async Task<IActionResult> ReserveSeat([FromBody] ReserveSeatRequest request)
          => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene la iformación de asientos disponibles
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("SeatAvailable")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetSeatAvailableResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetSeatAvailable([FromQuery] GetSeatAvailableRequest request)
          => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}