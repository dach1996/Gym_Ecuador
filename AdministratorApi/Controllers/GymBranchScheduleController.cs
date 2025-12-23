using Asp.Versioning;
using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicAdministratorApi.Model.Request.GymBranchSchedule;
using LogicAdministratorApi.Model.Response.GymBranchSchedule;

namespace AdministratorApi.Controllers;

/// <summary>
/// Controlador de Horarios de Sucursal de Gimnasio
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class GymBranchScheduleController(
    IUserMessages userMessages,
    ILogger<GymBranchScheduleController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{
    #region Methods Controller

    /// <summary>
    /// Crear Horario de Sucursal
    /// </summary>
    /// <param name="request">Modelo para crear horario de sucursal</param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("Create")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateGymBranchScheduleResponse>))]
    public async Task<IActionResult> CreateGymBranchSchedule([FromBody] CreateGymBranchScheduleRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Actualizar Horario de Sucursal
    /// </summary>
    /// <param name="request">Modelo para actualizar horario de sucursal</param>
    /// <returns></returns>
    [Authorize]
    [HttpPut("Update")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<UpdateGymBranchScheduleResponse>))]
    public async Task<IActionResult> UpdateGymBranchSchedule([FromBody] UpdateGymBranchScheduleRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Eliminar Horario de Sucursal
    /// </summary>
    /// <param name="request">Modelo para eliminar horario de sucursal</param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete("Delete")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<DeleteGymBranchScheduleResponse>))]
    public async Task<IActionResult> DeleteGymBranchSchedule([FromBody] DeleteGymBranchScheduleRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}

