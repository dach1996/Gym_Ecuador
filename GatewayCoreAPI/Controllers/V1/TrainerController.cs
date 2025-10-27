using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.Trainer;
using LogicApi.Model.Request.Trainer;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class TrainerController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public TrainerController(
        IUserMessages userMessages,
        ILogger<TrainerController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Obtiene la lista de entrenadores con paginación
    /// </summary>
    /// <param name="request">Modelo para obtener entrenadores</param>
    /// <returns></returns>
    [HttpGet("GetTrainers")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetTrainersResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetTrainers([FromQuery] GetTrainersRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene un entrenador por su GUID
    /// </summary>
    /// <param name="trainerGuid">GUID del entrenador</param>
    /// <returns></returns>
    [HttpGet("GetTrainerByGuid/{trainerGuid}")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetTrainerByGuidResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetTrainerByGuid([FromRoute] Guid trainerGuid)
    {
        var request = new GetTrainerByGuidRequest { TrainerGuid = trainerGuid };
        return Success(await Mediator.Send(request).ConfigureAwait(false));
    }

    /// <summary>
    /// Crea un nuevo entrenador
    /// </summary>
    /// <param name="request">Modelo para crear entrenador</param>
    /// <returns></returns>
    [HttpPost("CreateTrainer")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateTrainerResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateTrainer([FromBody] CreateTrainerRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Actualiza un entrenador existente
    /// </summary>
    /// <param name="request">Modelo para actualizar entrenador</param>
    /// <returns></returns>
    [HttpPut("UpdateTrainer")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<UpdateTrainerResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> UpdateTrainer([FromBody] UpdateTrainerRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
