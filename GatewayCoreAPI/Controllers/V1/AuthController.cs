using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.Authorization;
using LogicApi.Model.Request.Authorization;
using LogicApi.Model.Response;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AuthController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public AuthController(
        IUserMessages userMessages,
        ILogger<AuthController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Autenticación
    /// </summary>
    /// <param name="request">Modelo para autenticación</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Login")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<LoginResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Registrar Usuario
    /// </summary>
    /// <param name="request">Modelo para autenticación</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("CreateUser")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<HandlerResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Asignar una persona
    /// </summary>
    /// <param name="request">Modelo para autenticación</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("AssignPerson")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<LoginResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> AssignPerson([FromBody] AssignPersonRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));


    /// <summary>
    /// Actualiza información de usuario
    /// </summary>
    /// <param name="request">Modelo para autenticación</param>
    /// <returns></returns>
    [HttpPut("UpdateUser")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<HandlerResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Cierra sesión
    /// </summary>
    /// <returns></returns>
    [HttpPost("logout")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<HandlerResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Refrescar el token
    /// </summary>
    /// <returns></returns>
    [HttpPost("refreshToken")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<RefreshTokenResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Cambiar contraseña 
    /// </summary>
    /// <param name="request">Request de cambio de contraseña</param>
    /// <returns></returns>
    [HttpPost("password/change")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<HandlerResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> PasswordChange([FromBody] PasswordChangeRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Olvidó contraseña
    /// </summary>
    /// <param name="request">Olvidó contraseña</param>
    /// <returns></returns>
    [HttpPost("password/forgotten")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<HandlerResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    [AllowAnonymous]
    public async Task<IActionResult> PasswordForgotten([FromBody] PasswordForgottenRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Registrar Biométrico
    /// </summary>
    /// <param name="request">Registrar Biométrico</param>
    /// <returns></returns>
    [HttpPost("biometric/register")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<RegisterBiometricResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> BiometricRegister([FromBody] RegisterBiometricRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Borrar Biométrico
    /// </summary>
    /// <param name="request">Borrar Biométrico</param>
    /// <returns></returns>
    [HttpDelete("biometric")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<HandlerResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> RemoveBiomegric([FromBody] RemoveBiometricRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}