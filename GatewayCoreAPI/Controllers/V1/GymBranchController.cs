using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.GymBranch;
using LogicApi.Model.Request.GymBranch;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

/// <summary>
/// Constructor
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
    /// Obtiene la lista de sucursales de gimnasio con paginación
    /// </summary>
    /// <param name="request">Modelo para obtener sucursales</param>
    /// <returns></returns>
    [HttpGet("GetGymBranches")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetGymBranchesResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetGymBranches([FromQuery] GetGymBranchesRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene el detalle de una sucursal de gimnasio por GUID
    /// </summary>
    /// <param name="request">Modelo para obtener el detalle de una sucursal de gimnasio por GUID</param>
    /// <returns></returns>
    [HttpGet("GetGymBranchByGuid")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetGymBranchByGuidResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetGymBranchByGuid([FromQuery] GetGymBranchByGuidRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));
    #endregion
}

