using Asp.Versioning;
using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicAdministratorApi.Model.Request.Authorization;
using LogicAdministratorApi.Model.Response.Authorization;

namespace AdministratorApi.Controllers;
/// <summary>
/// Constructor
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AuthController(
    IUserMessages userMessages,
    ILogger<AuthController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{

    #region Methods Controller

    /// <summary>
    /// Autenticación
    /// </summary>
    /// <param name="request">Modelo para autenticación</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Login")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<LoginResponse>))]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}