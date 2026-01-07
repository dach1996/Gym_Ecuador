using Asp.Versioning;
using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicAdministratorApi.Model.Request.UserClient;
using LogicAdministratorApi.Model.Response.UserClient;

namespace AdministratorApi.Controllers;

/// <summary>
/// Controlador de Usuarios Clientes
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class UserClientController(
    IUserMessages userMessages,
    ILogger<UserClientController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{
    #region Methods Controller

    /// <summary>
    /// Obtener Usuarios Clientes Paginados
    /// </summary>
    /// <param name="request">Modelo para obtener usuarios clientes paginados</param>
    /// <returns></returns>
    [HttpGet("GetPaginated")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetUsersClientResponse>))]
    public async Task<IActionResult> GetUsersClientPaginated([FromQuery] GetUsersClientRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Crear Usuario Cliente
    /// </summary>
    /// <param name="request">Modelo para crear usuario cliente</param>
    /// <returns></returns>
    [HttpPost("Create")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateUserClientResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateUserClient([FromBody] CreateUserClientRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}

