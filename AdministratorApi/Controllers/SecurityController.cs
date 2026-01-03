using Asp.Versioning;
using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicAdministratorApi.Model.Request.Security;
using LogicAdministratorApi.Model.Response.Security;

namespace AdministratorApi.Controllers;
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class SecurityController : ApiControllerBase
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public SecurityController(
        IUserMessages userMessages,
        ILogger<SecurityController> logger,
        IMediator mediator
        ) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    /// <summary>
    /// Llave publica
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("publicKey")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetPublicKeyResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetPublicKey([FromQuery] GetPublicKeyRequest request)
         => Success(await Mediator.Send(request).ConfigureAwait(false));
}

