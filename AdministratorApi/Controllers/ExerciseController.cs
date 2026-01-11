using Asp.Versioning;
using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicAdministratorApi.Model.Request.Exercise;
using LogicAdministratorApi.Model.Response.Exercise;

namespace AdministratorApi.Controllers;

/// <summary>
/// Controlador de Ejercicios
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ExerciseController(
    IUserMessages userMessages,
    ILogger<ExerciseController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{
    #region Methods Controller

    /// <summary>
    /// Obtiene la lista de ejercicios con paginación
    /// </summary>
    /// <param name="request">Modelo para obtener ejercicios</param>
    /// <returns></returns>
    [HttpGet("GetExercises")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetExercisesResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetExercises([FromQuery] GetExercisesRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene el detalle de un ejercicio por GUID
    /// </summary>
    /// <param name="request">Modelo para obtener detalle de ejercicio por GUID</param>
    /// <returns></returns>
    [HttpGet("GetExerciseByGuid")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetExerciseByGuidResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetExerciseByGuid([FromQuery] GetExerciseByGuidRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Crea un nuevo ejercicio
    /// </summary>
    /// <param name="request">Modelo para crear ejercicio</param>
    /// <returns></returns>
    [HttpPost("CreateExercise")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateExerciseResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateExercise([FromBody] CreateExerciseRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Actualiza un ejercicio existente
    /// </summary>
    /// <param name="request">Modelo para actualizar ejercicio</param>
    /// <returns></returns>
    [HttpPut("UpdateExercise")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<UpdateExerciseResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> UpdateExercise([FromBody] UpdateExerciseRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
