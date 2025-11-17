using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Request.GymBranchSchedule;
using LogicApi.Model.Response.GymBranchSchedule;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class GymBranchScheduleController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public GymBranchScheduleController(
        IUserMessages userMessages,
        ILogger<GymBranchScheduleController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Crea un nuevo horario de sucursal
    /// </summary>
    /// <param name="request">Modelo para crear horario</param>
    /// <returns></returns>
    [HttpPost("Create")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateGymBranchScheduleResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateGymBranchSchedule([FromBody] CreateGymBranchScheduleRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Actualiza un horario de sucursal
    /// </summary>
    /// <param name="request">Modelo para actualizar horario</param>
    /// <returns></returns>
    [HttpPut("Update")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<UpdateGymBranchScheduleResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> UpdateGymBranchSchedule([FromBody] UpdateGymBranchScheduleRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene los horarios de una sucursal
    /// </summary>
    /// <param name="request">Modelo para obtener horarios</param>
    /// <returns></returns>
    [HttpGet("GetSchedules")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetGymBranchSchedulesResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetGymBranchSchedules([FromQuery] GetGymBranchSchedulesRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Elimina un horario de sucursal
    /// </summary>
    /// <param name="request">Modelo para eliminar horario</param>
    /// <returns></returns>
    [HttpDelete("Delete")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<DeleteGymBranchScheduleResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> DeleteGymBranchSchedule([FromBody] DeleteGymBranchScheduleRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}

