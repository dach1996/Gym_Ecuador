using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.Forum;
using LogicApi.Model.Request.Forum;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

/// <summary>
/// Controlador de Foro
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ForumController(
    IUserMessages userMessages,
    ILogger<ForumController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{
    #region Methods Controller

    /// <summary>
    /// Obtiene la lista de foros con paginación
    /// </summary>
    /// <param name="request">Modelo para obtener foros</param>
    /// <returns></returns>
    [HttpGet("GetForums")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetForumsResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetForums([FromQuery] GetForumsRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Crea un nuevo foro
    /// </summary>
    /// <param name="request">Modelo para crear foro</param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("Create")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateForumResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateForum([FromBody] CreateForumRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Actualiza un foro
    /// </summary>
    /// <param name="request">Modelo para actualizar foro</param>
    /// <returns></returns>
    [Authorize]
    [HttpPut("Update")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<UpdateForumResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> UpdateForum([FromBody] UpdateForumRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Comenta en un foro
    /// </summary>
    /// <param name="request">Modelo para comentar en foro</param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("Comment")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CommentForumResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CommentForum([FromBody] CommentForumRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}

