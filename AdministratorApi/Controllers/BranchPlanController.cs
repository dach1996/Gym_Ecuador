using Asp.Versioning;
using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicAdministratorApi.Model.Request.BranchPlan;
using LogicAdministratorApi.Model.Response.BranchPlan;

namespace AdministratorApi.Controllers;

/// <summary>
/// Controlador de Planes de Sucursal
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class BranchPlanController(
    IUserMessages userMessages,
    ILogger<BranchPlanController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{
    #region Methods Controller

    /// <summary>
    /// Crear Plan de Sucursal
    /// </summary>
    /// <param name="request">Modelo para crear plan de sucursal</param>
    /// <returns></returns>
    [HttpPost("Create")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateBranchPlanResponse>))]
    public async Task<IActionResult> CreateBranchPlan([FromBody] CreateBranchPlanRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Actualizar Plan de Sucursal
    /// </summary>
    /// <param name="request">Modelo para actualizar plan de sucursal</param>
    /// <returns></returns>
    [HttpPut("Update")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<UpdateBranchPlanResponse>))]
    public async Task<IActionResult> UpdateBranchPlan([FromBody] UpdateBranchPlanRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Eliminar Plan de Sucursal
    /// </summary>
    /// <param name="request">Modelo para eliminar plan de sucursal</param>
    /// <returns></returns>
    [HttpDelete("Delete")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<DeleteBranchPlanResponse>))]
    public async Task<IActionResult> DeleteBranchPlan([FromBody] DeleteBranchPlanRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtener Plan de Sucursal por GUID
    /// </summary>
    /// <param name="request">Modelo para obtener plan de sucursal por GUID</param>
    /// <returns></returns>
    [HttpGet("GetByGuid")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetBranchPlanByGuidResponse>))]
    public async Task<IActionResult> GetBranchPlanByGuid([FromQuery] GetBranchPlanByGuidRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtener Planes de Sucursal Paginados por GUID de Sucursal
    /// </summary>
    /// <param name="request">Modelo para obtener planes de sucursal paginados</param>
    /// <returns></returns>
    [HttpGet("GetPaginatedByGymBranchGuid")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetBranchPlansResponse>))]
    public async Task<IActionResult> GetBranchPlansPaginatedByGymBranchGuid([FromQuery] GetBranchPlansRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}

