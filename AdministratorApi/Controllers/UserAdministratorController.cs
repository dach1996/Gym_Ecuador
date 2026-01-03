using Asp.Versioning;
using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicAdministratorApi.Model.Request.UserAdministrator;
using LogicAdministratorApi.Model.Response.UserAdministrator;

namespace AdministratorApi.Controllers;

/// <summary>
/// Controlador de Usuarios Administradores
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class UserAdministratorController(
    IUserMessages userMessages,
    ILogger<UserAdministratorController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{
    #region Methods Controller

    /// <summary>
    /// Crear Usuario Administrador
    /// </summary>
    /// <param name="request">Modelo para crear usuario administrador</param>
    /// <returns></returns>
    [HttpPost("Create")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateUserAdministratorResponse>))]
    public async Task<IActionResult> CreateUserAdministrator([FromBody] CreateUserAdministratorRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Actualizar Usuario Administrador
    /// </summary>
    /// <param name="request">Modelo para actualizar usuario administrador</param>
    /// <returns></returns>
    [HttpPut("Update")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<UpdateUserAdministratorResponse>))]
    public async Task<IActionResult> UpdateUserAdministrator([FromBody] UpdateUserAdministratorRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Eliminar Usuario Administrador
    /// </summary>
    /// <param name="request">Modelo para eliminar usuario administrador</param>
    /// <returns></returns>
    [HttpDelete("Delete")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<DeleteUserAdministratorResponse>))]
    public async Task<IActionResult> DeleteUserAdministrator([FromBody] DeleteUserAdministratorRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtener Usuario Administrador por GUID
    /// </summary>
    /// <param name="request">Modelo para obtener usuario administrador por GUID</param>
    /// <returns></returns>
    [HttpGet("GetByGuid")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetUserAdministratorByGuidResponse>))]
    public async Task<IActionResult> GetUserAdministratorByGuid([FromQuery] GetUserAdministratorByGuidRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtener Usuarios Administradores Paginados
    /// </summary>
    /// <param name="request">Modelo para obtener usuarios administradores paginados</param>
    /// <returns></returns>
    [HttpGet("GetPaginated")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetUsersAdministratorResponse>))]
    public async Task<IActionResult> GetUsersAdministratorPaginated([FromQuery] GetUsersAdministratorRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}

