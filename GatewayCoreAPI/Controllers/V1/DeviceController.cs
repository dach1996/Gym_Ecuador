using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Request.Device;
using LogicApi.Model.Response;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class DeviceController : SecurityControllerBase
{
    #region Constructor

    /// <summary>
    /// Constructor
    /// </summary>
    public DeviceController(
        IUserMessages userMessages,
        ILogger<DeviceController> logger,
        IMediator mediator
        ) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Actualiza el push token
    /// </summary>
    /// <param name="request">Request de actualización</param>
    /// <returns></returns>
    [HttpPost("pushToken")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<HandlerResponse>))]
    [ProducesResponseType(200, Type = typeof(GenericResponse))]
    public async Task<IActionResult> PushToken([FromBody] RegisterPushTokenRequest request)
         => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}