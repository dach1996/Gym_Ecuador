using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.ProcessTracking;
using LogicApi.Model.Request.ProcessTracking;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ProcessTrackingController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public ProcessTrackingController(
        IUserMessages userMessages,
        ILogger<ProcessTrackingController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

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
    /// <param name="processTrackingGuid">GUID del seguimiento de proceso</param>
    /// <returns></returns>
    [HttpGet("GetProcessTrackingByGuid/{processTrackingGuid}")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetProcessTrackingByGuidResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetProcessTrackingByGuid([FromRoute] Guid processTrackingGuid)
    {
        var request = new GetProcessTrackingByGuidRequest { ProcessTrackingGuid = processTrackingGuid };
        return Success(await Mediator.Send(request).ConfigureAwait(false));
    }

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

    #endregion
}
