using Asp.Versioning;
using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicAdministratorApi.Model.Request.ClientMembership;
using LogicAdministratorApi.Model.Response.ClientMembership;

namespace AdministratorApi.Controllers;

/// <summary>
/// Controlador de Membresías de Clientes
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
    /// Obtener Membresías de Clientes Paginadas por Gimnasio y Sucursal
    /// </summary>
    /// <param name="request">Modelo para obtener membresías de clientes paginadas</param>
    /// <returns></returns>
    [HttpGet("GetPaginatedByGymAndBranch")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetClientMembershipsResponse>))]
    public async Task<IActionResult> GetClientMembershipsPaginatedByGymAndBranch([FromQuery] GetClientMembershipsRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
