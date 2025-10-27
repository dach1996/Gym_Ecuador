using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.ClassReservation;
using LogicApi.Model.Request.ClassReservation;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ClassReservationController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public ClassReservationController(
        IUserMessages userMessages,
        ILogger<ClassReservationController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Crea una nueva reserva de clase
    /// </summary>
    /// <param name="request">Modelo para crear reserva de clase</param>
    /// <returns></returns>
    [HttpPost("CreateClassReservation")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateClassReservationResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateClassReservation([FromBody] CreateClassReservationRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Cancela una reserva de clase existente
    /// </summary>
    /// <param name="request">Modelo para cancelar reserva de clase</param>
    /// <returns></returns>
    [HttpPut("CancelClassReservation")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CancelClassReservationResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CancelClassReservation([FromBody] CancelClassReservationRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
