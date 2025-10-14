using Asp.Versioning;
using Common.Messages;
using Common.WebApi.Controller;
using Common.WebApi.Models;
using LogicApi.Model.Request.Logger;
using LogicApi.Model.Response.Logger;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace GatewayCoreAPI.Controllers.V1;
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class LoggerController : SecurityControllerBase
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public LoggerController(
        IUserMessages userMessages,
        ILogger<LoggerController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    /// <summary>
    /// Permite registrar logs desde dispositivos
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("RegisterLog")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<RegisterLogResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> RegisterLog(RegisterLogRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));
}
