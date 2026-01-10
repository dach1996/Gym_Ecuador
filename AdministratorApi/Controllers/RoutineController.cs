using Asp.Versioning;
using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicAdministratorApi.Model.Request.Routine;
using LogicAdministratorApi.Model.Response.Routine;

namespace AdministratorApi.Controllers;

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
    /// Obtiene las rutinas creadas por el administrador con paginación
    /// </summary>
    /// <param name="request">Modelo para obtener rutinas creadas por el admin</param>
    /// <returns></returns>
    [HttpGet("GetRoutinesCreatedByAdmin")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetRoutinesCreatedByAdminResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetRoutinesCreatedByAdmin([FromQuery] GetRoutinesCreatedByAdminRequest request)
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
    /// Crea una nueva rutina con ejercicios asignada a un usuario
    /// </summary>
    /// <param name="request">Modelo para crear rutina con ejercicios para un usuario</param>
    /// <returns></returns>
    [HttpPost("CreateRoutineWithExercisesForUser")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateRoutineWithExercisesForUserResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateRoutineWithExercisesForUser([FromBody] CreateRoutineWithExercisesForUserRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
