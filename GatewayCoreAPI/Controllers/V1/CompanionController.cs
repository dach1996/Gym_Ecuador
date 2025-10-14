using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.Companion;
using LogicApi.Model.Request.Companion;
using LogicApi.Model.Response;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CompanionController : SecurityControllerBase
{
    /// <summary>
    /// Constructor
    /// </summary>
    public CompanionController(
        IUserMessages userMessages,
        ILogger<CompanionController> logger,
        IMediator mediator
        ) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #region Methods Controller

    /// <summary>
    /// Crear Compañero
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateOrUpdateCompanionResponse>))]
    public async Task<IActionResult> CreateOrUpdateCompanion([FromBody] CreateOrUpdateCompanionRequest request)
          => Success(await Mediator.Send(request).ConfigureAwait(false));


    /// <summary>
    /// Obteniene compañeros registrados
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("MyCompanion")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetMyCompanionResponse>))]
    public async Task<IActionResult> GetMyCompanion([FromQuery] GetMyCompanionRequest request)
          => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Busca compañero de viaje
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("Search")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<SearchCompanionResponse>))]
    public async Task<IActionResult> SearchCompanion([FromQuery] SearchCompanionRequest request)
          => Success(await Mediator.Send(request).ConfigureAwait(false));
    #endregion
}