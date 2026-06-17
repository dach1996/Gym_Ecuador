using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.Profile;
using LogicApi.Model.Request.Profile;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ProfileController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public ProfileController(
        IUserMessages userMessages,
        ILogger<ProfileController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Calcula proteína, carbohidratos y grasas según datos del perfil
    /// </summary>
    /// <param name="request">Modelo para calcular macros del perfil</param>
    /// <returns></returns>
    [HttpPost("CalculateProfile")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CalculateProfileResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CalculateProfile([FromBody] CalculateProfileRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Crea un perfil completo
    /// </summary>
    /// <param name="request">Modelo para crear perfil</param>
    /// <returns></returns>
    [HttpPost("CreateProfile")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateProfileResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateProfile([FromBody] CreateProfileRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
