using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.Exercise;
using LogicApi.Model.Request.Exercise;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

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

    #endregion
}
