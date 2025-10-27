using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.GroupClass;
using LogicApi.Model.Request.GroupClass;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class GroupClassController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public GroupClassController(
        IUserMessages userMessages,
        ILogger<GroupClassController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Crea una nueva clase grupal
    /// </summary>
    /// <param name="request">Modelo para crear clase grupal</param>
    /// <returns></returns>
    [HttpPost("CreateGroupClass")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateGroupClassResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateGroupClass([FromBody] CreateGroupClassRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
