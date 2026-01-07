using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using Asp.Versioning;
using LogicAdministratorApi.Model.Request.Person;
using LogicCommon.Model.Response.Person;

namespace AdministratorApi.Controllers;

/// <summary>
/// Controlador de Persona
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class PersonController(
    IUserMessages userMessages,
    ILogger<PersonController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{

    /// <summary>
    /// Obtiene una persona por su número de cédula
    /// </summary>
    /// <param name="request">Modelo para obtener persona por número de cédula</param>
    /// <returns></returns>
    [HttpGet("GetByDocumentNumber")]
    [AllowAnonymous]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetPersonByDocumentNumberResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetPersonByDocumentNumber([FromQuery] GetPersonByDocumentNumberRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));
}

