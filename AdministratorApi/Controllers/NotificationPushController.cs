using Asp.Versioning;
using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicAdministratorApi.Model.Request.NotificationPush;
using LogicAdministratorApi.Model.Response.NotificationPush;

namespace AdministratorApi.Controllers;

/// <summary>
/// Controlador de Notificaciones Push
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class NotificationPushController(
    IUserMessages userMessages,
    ILogger<NotificationPushController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{
    #region Methods Controller

    /// <summary>
    /// Enviar Notificación Push por UserGuids
    /// </summary>
    /// <param name="request">Modelo para enviar notificación push por UserGuids</param>
    /// <returns></returns>
    [HttpPost("SendByUserGuids")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<SendNotificationPushByUserGuidsResponse>))]
    public async Task<IActionResult> SendNotificationPushByUserGuids([FromBody] SendNotificationPushByUserGuidsRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtener Notificaciones Push enviadas de manera paginada
    /// </summary>
    /// <param name="request">Modelo para obtener notificaciones push paginadas</param>
    /// <returns></returns>
    [HttpGet("GetNotificationPushes")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetNotificationPushesResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetNotificationPushes([FromQuery] GetNotificationPushesRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}

