using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.Administration;
using LogicApi.Model.Request.Administration;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AdministrationController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public AdministrationController(
        IUserMessages userMessages,
        ILogger<AdministrationController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Obtiene los catálogos inciales
    /// </summary>
    /// <param name="request">Modelo para Obtiene los catálogos inciales</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("GetInitialCatalogues")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetInitialCataloguesResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetInitialCatalogues([FromQuery] GetInitialCataloguesRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}