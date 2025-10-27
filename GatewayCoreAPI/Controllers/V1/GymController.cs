using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.Gym;
using LogicApi.Model.Request.Gym;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class GymController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public GymController(
        IUserMessages userMessages,
        ILogger<GymController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Obtiene la lista de gimnasios con paginación
    /// </summary>
    /// <param name="request">Modelo para obtener gimnasios</param>
    /// <returns></returns>
    [HttpGet("GetGyms")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetGymsResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetGyms([FromQuery] GetGymsRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene un gimnasio por su GUID
    /// </summary>
    /// <param name="gymGuid">GUID del gimnasio</param>
    /// <returns></returns>
    [HttpGet("GetGymByGuid/{gymGuid}")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetGymByGuidResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetGymByGuid([FromRoute] Guid gymGuid)
    {
        var request = new GetGymByGuidRequest { GymGuid = gymGuid };
        return Success(await Mediator.Send(request).ConfigureAwait(false));
    }

    /// <summary>
    /// Crea un nuevo gimnasio
    /// </summary>
    /// <param name="request">Modelo para crear gimnasio</param>
    /// <returns></returns>
    [HttpPost("CreateGym")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateGymResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateGym([FromBody] CreateGymRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Actualiza un gimnasio existente
    /// </summary>
    /// <param name="request">Modelo para actualizar gimnasio</param>
    /// <returns></returns>
    [HttpPut("UpdateGym")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<UpdateGymResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> UpdateGym([FromBody] UpdateGymRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
