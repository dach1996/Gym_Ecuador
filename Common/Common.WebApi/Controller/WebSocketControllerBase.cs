using Common.Messages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
namespace Common.WebApi.Controller;

/// <summary>
/// Clase para WebSockets
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
/// <returns></returns>
[Authorize]
public class WebSocketControllerBase<T>(
    IUserMessages userMessages,
    ILogger<WebSocketControllerBase<T>> logger,
    IHubContext<T> hubContext,
    IMediator mediator) : WebSocketControllerBase(userMessages, logger, mediator) where T : Hub
{
    protected readonly IHubContext<T> HubContext = hubContext;
}

/// <summary>
/// Clase para WebSockets
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
/// <returns></returns>
public class WebSocketControllerBase(
    IUserMessages userMessages,
    ILogger<WebSocketControllerBase> logger,
    IMediator mediator) : GenericControlerBase(userMessages, logger, mediator)
{
}

