using Asp.Versioning;
using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicAdministratorApi.Model.Request.Role;
using LogicAdministratorApi.Model.Response.Role;
using LogicAdministratorApi.Model.Request.Functionality;
using LogicAdministratorApi.Model.Response.Functionality;

namespace AdministratorApi.Controllers;

/// <summary>
/// Controlador de Roles y Funcionalidades
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class RoleController(
    IUserMessages userMessages,
    ILogger<RoleController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{
    #region Methods Controller

    /// <summary>
    /// Obtener todos los roles
    /// </summary>
    /// <param name="request">Modelo para obtener roles</param>
    /// <returns></returns>
    [HttpPost("GetRoles")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetRolesResponse>))]
    public async Task<IActionResult> GetRoles([FromBody] GetRolesRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtener detalle de rol por GUID
    /// </summary>
    /// <param name="request">Modelo para obtener detalle de rol</param>
    /// <returns></returns>
    [HttpPost("GetRoleDetail")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetRoleDetailResponse>))]
    public async Task<IActionResult> GetRoleDetail([FromBody] GetRoleDetailRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtener todas las funcionalidades
    /// </summary>
    /// <param name="request">Modelo para obtener funcionalidades</param>
    /// <returns></returns>
    [HttpPost("GetFunctionalities")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetFunctionalitiesResponse>))]
    public async Task<IActionResult> GetFunctionalities([FromBody] GetFunctionalitiesRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Actualizar un rol
    /// </summary>
    /// <param name="request">Modelo para actualizar un rol</param>
    /// <returns></returns>
    [HttpPut("Update")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<UpdateRoleResponse>))]
    public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Crear un nuevo rol
    /// </summary>
    /// <param name="request">Modelo para crear un rol</param>
    /// <returns></returns>
    [HttpPost("Create")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateRoleResponse>))]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
