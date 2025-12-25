using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.Article;
using LogicApi.Model.Request.Article;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

/// <summary>
/// Controlador de Artículos
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ArticleController(
    IUserMessages userMessages,
    ILogger<ArticleController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{
    #region Methods Controller

    /// <summary>
    /// Obtiene la lista de artículos con paginación
    /// </summary>
    /// <param name="request">Modelo para obtener artículos</param>
    /// <returns></returns>
    [HttpGet("GetArticles")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetArticlesResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetArticles([FromQuery] GetArticlesRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}

