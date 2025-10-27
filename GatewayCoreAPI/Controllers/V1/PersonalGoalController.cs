using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.PersonalGoal;
using LogicApi.Model.Request.PersonalGoal;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class PersonalGoalController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public PersonalGoalController(
        IUserMessages userMessages,
        ILogger<PersonalGoalController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Obtiene la lista de objetivos personales con paginación
    /// </summary>
    /// <param name="request">Modelo para obtener objetivos personales</param>
    /// <returns></returns>
    [HttpGet("GetPersonalGoals")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetPersonalGoalsResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetPersonalGoals([FromQuery] GetPersonalGoalsRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Crea un nuevo objetivo personal
    /// </summary>
    /// <param name="request">Modelo para crear objetivo personal</param>
    /// <returns></returns>
    [HttpPost("CreatePersonalGoal")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreatePersonalGoalResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreatePersonalGoal([FromBody] CreatePersonalGoalRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Actualiza un objetivo personal existente
    /// </summary>
    /// <param name="request">Modelo para actualizar objetivo personal</param>
    /// <returns></returns>
    [HttpPut("UpdatePersonalGoal")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<UpdatePersonalGoalResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> UpdatePersonalGoal([FromBody] UpdatePersonalGoalRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
