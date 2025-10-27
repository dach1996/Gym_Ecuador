using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.ClassSchedule;
using LogicApi.Model.Request.ClassSchedule;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ClassScheduleController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public ClassScheduleController(
        IUserMessages userMessages,
        ILogger<ClassScheduleController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Crea un nuevo horario de clase
    /// </summary>
    /// <param name="request">Modelo para crear horario de clase</param>
    /// <returns></returns>
    [HttpPost("CreateClassSchedule")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateClassScheduleResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateClassSchedule([FromBody] CreateClassScheduleRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
