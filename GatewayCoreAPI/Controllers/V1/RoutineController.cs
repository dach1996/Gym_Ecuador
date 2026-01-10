using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.Routine;
using LogicApi.Model.Request.Routine;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

/// <summary>
/// Controlador de Rutinas
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class RoutineController(
    IUserMessages userMessages,
    ILogger<RoutineController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{
    #region Methods Controller

    /// <summary>
    /// Obtiene la lista de mis rutinas con paginación
    /// </summary>
    /// <param name="request">Modelo para obtener mis rutinas</param>
    /// <returns></returns>
    [HttpGet("GetMyRoutines")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetMyRoutinesResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetMyRoutines([FromQuery] GetMyRoutinesRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene los ejercicios de una rutina por GUID
    /// </summary>
    /// <param name="request">Modelo para obtener ejercicios de rutina por GUID</param>
    /// <returns></returns>
    [HttpGet("GetRoutineExercisesByGuid")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetRoutineExercisesByGuidResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetRoutineExercisesByGuid([FromQuery] GetRoutineExercisesByGuidRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Crea una nueva rutina con ejercicios
    /// </summary>
    /// <param name="request">Modelo para crear rutina con ejercicios</param>
    /// <returns></returns>
    [HttpPost("CreateRoutineWithExercises")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateRoutineWithExercisesResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateRoutineWithExercises([FromBody] CreateRoutineWithExercisesRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
