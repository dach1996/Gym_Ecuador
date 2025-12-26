using Asp.Versioning;
using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicAdministratorApi.Model.Request.GymBranch;
using LogicAdministratorApi.Model.Response.GymBranch;

namespace AdministratorApi.Controllers;

/// <summary>
/// Controlador de Sucursales de Gimnasio
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class GymBranchController(
    IUserMessages userMessages,
    ILogger<GymBranchController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{
    #region Methods Controller

    /// <summary>
    /// Crear Sucursal de Gimnasio
    /// </summary>
    /// <param name="request">Modelo para crear sucursal de gimnasio</param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("Create")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateGymBranchResponse>))]
    public async Task<IActionResult> CreateGymBranch([FromBody] CreateGymBranchRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Actualizar Sucursal de Gimnasio
    /// </summary>
    /// <param name="request">Modelo para actualizar sucursal de gimnasio</param>
    /// <returns></returns>
    [Authorize]
    [HttpPut("Update")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<UpdateGymBranchResponse>))]
    public async Task<IActionResult> UpdateGymBranch([FromBody] UpdateGymBranchRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}

