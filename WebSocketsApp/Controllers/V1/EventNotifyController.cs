using Asp.Versioning;
using Common.Messages;
using Common.WebApi.Controller;
using Common.WebApi.Models;
using LogicWebSocket.Model.Request.EventNotify;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebSocketsApp.Hubs;

namespace WebSocketsApp.Controllers.V1;
/// <summary>
/// Constructor
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="hubContext"></param>p
/// <param name="mediator"></param>
/// <returns></returns>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class EventNotifyController(
    IUserMessages userMessages,
    ILogger<GenericControlerBase> logger,
    IHubContext<HubManager> hubContext,
    IMediator mediator) : WebSocketControllerBase<HubManager>(userMessages, logger, hubContext, mediator)
{

    #region Methods Controller

    /// <summary>
    /// Envía una notificación por el Hub de Cooperativas
    /// </summary>
    /// <param name="request">Modelo para enviar notificación</param>
    /// <returns></returns>
    [HttpPost("Group")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<string>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> Group([FromBody] NotifyEventGroupRequest request)
    {
        await HubContext.Clients.Groups(request.GroupName).SendAsync(request.EventName, request.DataModel);
        return NoContent();
    }
    #endregion
}