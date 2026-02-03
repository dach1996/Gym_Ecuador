using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.ClientMembership;
using LogicApi.Model.Request.ClientMembership;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

/// <summary>
/// Controller de membresías de cliente
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ClientMembershipController(
    IUserMessages userMessages,
    ILogger<ClientMembershipController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{

    #region Methods Controller

    /// <summary>
    /// Obtiene mis membresías agrupadas por sucursal de gimnasio
    /// </summary>
    /// <param name="request">Modelo para obtener mis membresías</param>
    /// <returns></returns>
    [HttpGet("GetMyMemberships")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetMyMembershipsResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetMyMemberships([FromQuery] GetMyMembershipsRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
