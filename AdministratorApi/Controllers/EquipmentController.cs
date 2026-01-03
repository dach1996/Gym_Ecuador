using Asp.Versioning;
using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicAdministratorApi.Model.Request.Equipment;
using LogicAdministratorApi.Model.Response.Equipment;

namespace AdministratorApi.Controllers;

/// <summary>
/// Controlador de Equipamientos
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class EquipmentController(
    IUserMessages userMessages,
    ILogger<EquipmentController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{
    #region Methods Controller

    /// <summary>
    /// Crear Equipamiento
    /// </summary>
    /// <param name="request">Modelo para crear equipamiento</param>
    /// <returns></returns>
    [HttpPost("Create")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateEquipmentResponse>))]
    public async Task<IActionResult> CreateEquipment([FromBody] CreateEquipmentRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Actualizar Equipamiento
    /// </summary>
    /// <param name="request">Modelo para actualizar equipamiento</param>
    /// <returns></returns>
    [HttpPut("Update")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<UpdateEquipmentResponse>))]
    public async Task<IActionResult> UpdateEquipment([FromBody] UpdateEquipmentRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Eliminar Equipamiento
    /// </summary>
    /// <param name="request">Modelo para eliminar equipamiento</param>
    /// <returns></returns>
    [HttpDelete("Delete")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<DeleteEquipmentResponse>))]
    public async Task<IActionResult> DeleteEquipment([FromBody] DeleteEquipmentRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtener Equipamiento por GUID
    /// </summary>
    /// <param name="request">Modelo para obtener equipamiento por GUID</param>
    /// <returns></returns>
    [HttpGet("GetByGuid")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetEquipmentByGuidResponse>))]
    public async Task<IActionResult> GetEquipmentByGuid([FromQuery] GetEquipmentByGuidRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtener Equipamientos Paginados por GUID de Sucursal
    /// </summary>
    /// <param name="request">Modelo para obtener equipamientos paginados</param>
    /// <returns></returns>
    [HttpGet("GetPaginatedByGymBranchGuid")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetEquipmentsResponse>))]
    public async Task<IActionResult> GetEquipmentsPaginatedByGymBranchGuid([FromQuery] GetEquipmentsRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}

