using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using MediatR;
using Common.WebApi.Controller;
using LogicApi.Model.Response.Security;
using LogicApi.Model.Request.Security;
using Common.WebApi.Attributes.ValidateDevelopServices;
using LogicApi.Model.Response;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;
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

    /// <summary>
    /// Llave publica
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("ValidateDocument")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<HandlerResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> DocumentValidation([FromQuery] DocumentValidationRequest request)
         => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Encrypt (Solo para uso de desarrollo)
    /// </summary>
    /// <returns></returns>
    [HttpPost("encrypt")]
    [AllowAnonymous]
    [ProducesResponseType(200, Type = typeof(GenericResponse<EncryptResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    [OnlyDevelop]
    public async Task<IActionResult> Encrypt([FromBody] EncryptRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Decrypt (Solo para uso de desarrollo)
    /// </summary>
    /// <returns></returns>
    [HttpPost("decrypt")]
    [AllowAnonymous]
    [ProducesResponseType(200, Type = typeof(GenericResponse<DecryptResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    [OnlyDevelop]
    public async Task<IActionResult> Decrypt([FromBody] DecryptRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

}