using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Request.GymBranchSchedule;
using LogicApi.Model.Response.GymBranchSchedule;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class GymBranchScheduleController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public GymBranchScheduleController(
        IUserMessages userMessages,
        ILogger<GymBranchScheduleController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Obtiene los horarios de una sucursal
    /// </summary>
    /// <param name="request">Modelo para obtener horarios</param>
    /// <returns></returns>
    [HttpGet("GetSchedules")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetGymBranchSchedulesResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetGymBranchSchedules([FromQuery] GetGymBranchSchedulesRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));


    #endregion
}

