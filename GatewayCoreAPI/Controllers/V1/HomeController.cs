using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.Home;
using LogicApi.Model.Request.Home;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

/// <summary>
/// Controlador de Home/Dashboard
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class HomeController(
    IUserMessages userMessages,
    ILogger<HomeController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{
    #region Methods Controller

    /// <summary>
    /// Obtiene los datos del home/dashboard
    /// </summary>
    /// <param name="request">Modelo para obtener datos del home/dashboard</param>
    /// <returns></returns>
    [HttpGet("GetHomeData")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetHomeDataResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetHomeData([FromQuery] GetHomeDataRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
