using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.GymMachine;
using LogicApi.Model.Request.GymMachine;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class GymMachineController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public GymMachineController(
        IUserMessages userMessages,
        ILogger<GymMachineController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Crea una nueva máquina de gimnasio
    /// </summary>
    /// <param name="request">Modelo para crear máquina</param>
    /// <returns></returns>
    [HttpPost("CreateGymMachine")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateGymMachineResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateGymMachine([FromBody] CreateGymMachineRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Actualiza una máquina de gimnasio existente
    /// </summary>
    /// <param name="request">Modelo para actualizar máquina</param>
    /// <returns></returns>
    [HttpPut("UpdateGymMachine")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<UpdateGymMachineResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> UpdateGymMachine([FromBody] UpdateGymMachineRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
