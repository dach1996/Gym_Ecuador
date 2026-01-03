using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicCommon.Model.Response.Administration;
using Asp.Versioning;
using LogicAdministratorApi.Model.Request.Administration;

namespace AdministratorApi.Controllers;
/// <summary>
/// Controlador de Administración
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AdministrationController(
    IUserMessages userMessages,
    ILogger<AdministrationController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{

    /// <summary>
    /// Obtiene los catálogos inciales
    /// </summary>
    /// <param name="request">Modelo para Obtiene los catálogos inciales</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("GetInitialCatalogues")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetInitialCataloguesResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetInitialCatalogues([FromQuery] GetInitialCataloguesRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));
}