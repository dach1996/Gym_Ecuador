using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.BranchEquipment;
using LogicApi.Model.Request.BranchEquipment;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

/// <summary>
/// Controlador de Equipos de Sucursal
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class BranchEquipmentController(
    IUserMessages userMessages,
    ILogger<BranchEquipmentController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{

    #region Methods Controller

    /// <summary>
    /// Obtiene la lista de equipos de sucursal con paginación
    /// </summary>
    /// <param name="request">Modelo para obtener equipos de sucursal</param>
    /// <returns></returns>
    [HttpGet("GetBranchEquipments")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetBranchEquipmentsResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetBranchEquipments([FromQuery] GetBranchEquipmentsRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene el detalle de un equipo de sucursal por GUID
    /// </summary>
    /// <param name="request">Modelo para obtener el detalle de un equipo de sucursal por GUID</param>
    /// <returns></returns>
    [HttpGet("GetBranchEquipmentByGuid")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetBranchEquipmentByGuidResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetBranchEquipmentByGuid([FromQuery] GetBranchEquipmentByGuidRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}

