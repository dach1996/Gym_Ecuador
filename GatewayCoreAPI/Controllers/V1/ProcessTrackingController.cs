using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.ProcessTracking;
using LogicApi.Model.Request.ProcessTracking;
using Asp.Versioning;
using LogicCommon.Model.Response;

namespace GatewayCoreAPI.Controllers.V1;

/// <summary>
/// Constructor
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ProcessTrackingController(
    IUserMessages userMessages,
    ILogger<ProcessTrackingController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{

    /// <summary>
    /// Obtiene la lista de seguimientos de procesos con paginación
    /// </summary>
    /// <param name="request">Modelo para obtener seguimientos de procesos</param>
    /// <returns></returns>
    [HttpGet("GetProcessTrackings")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetProcessTrackingsResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetProcessTrackings([FromQuery] GetProcessTrackingsRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene un seguimiento de proceso por su GUID
    /// </summary>
    /// <param name="request">Modelo para obtener seguimiento de proceso por GUID</param>
    /// <returns></returns>
    [HttpGet("GetProcessTrackingByGuid")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetProcessTrackingByGuidResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetProcessTrackingByGuid([FromQuery] GetProcessTrackingByGuidRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene el seguimiento de proceso más reciente del usuario autenticado
    /// </summary>
    /// <param name="request">Modelo para obtener el seguimiento de proceso actual</param>
    /// <returns></returns>
    [HttpGet("GetCurrentProcessTracking")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetCurrentProcessTrackingResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetCurrentProcessTracking([FromQuery] GetCurrentProcessTrackingRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene estadísticas de seguimientos de procesos
    /// </summary>
    /// <param name="request">Modelo para obtener estadísticas de seguimientos de procesos</param>
    /// <returns></returns>
    [HttpGet("GetProcessTrackingStatistics")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetProcessTrackingStatisticsResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetProcessTrackingStatistics([FromQuery] GetProcessTrackingStatisticsRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Crea un nuevo seguimiento de proceso
    /// </summary>
    /// <param name="request">Modelo para crear seguimiento de proceso</param>
    /// <returns></returns>
    [HttpPost("CreateProcessTracking")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateProcessTrackingResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateProcessTracking([FromBody] CreateProcessTrackingRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Actualiza un seguimiento de proceso existente
    /// </summary>
    /// <param name="request">Modelo para actualizar seguimiento de proceso</param>
    /// <returns></returns>
    [HttpPut("UpdateProcessTracking")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<UpdateProcessTrackingResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> UpdateProcessTracking([FromBody] UpdateProcessTrackingRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Elimina un seguimiento de proceso existente
    /// </summary>
    /// <param name="request">Modelo para eliminar seguimiento de proceso</param>
    /// <returns></returns>
    [HttpDelete("DeleteProcessTracking")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GenericCommonOperationResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> DeleteProcessTracking([FromQuery] DeleteProcessTrackingRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

}
