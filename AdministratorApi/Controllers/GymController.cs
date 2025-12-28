using Asp.Versioning;
using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicAdministratorApi.Model.Request.Gym;
using LogicAdministratorApi.Model.Response.Gym;

namespace AdministratorApi.Controllers;

/// <summary>
/// Controlador de Gimnasios
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class GymController(
    IUserMessages userMessages,
    ILogger<GymController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{
    #region Methods Controller

    /// <summary>
    /// Crear Gimnasio
    /// </summary>
    /// <param name="request">Modelo para crear gimnasio</param>
    /// <returns></returns>
    [HttpPost("Create")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateGymResponse>))]
    public async Task<IActionResult> CreateGym([FromBody] CreateGymRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Actualizar Gimnasio
    /// </summary>
    /// <param name="request">Modelo para actualizar gimnasio</param>
    /// <returns></returns>
    [HttpPut("Update")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<UpdateGymResponse>))]
    public async Task<IActionResult> UpdateGym([FromBody] UpdateGymRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtener Gimnasios
    /// </summary>
    /// <param name="request">Modelo para obtener gimnasios</param>
    /// <returns></returns>
    [HttpGet("GetGyms")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetGymsResponse>))]
    public async Task<IActionResult> GetGyms([FromQuery] GetGymsRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}

