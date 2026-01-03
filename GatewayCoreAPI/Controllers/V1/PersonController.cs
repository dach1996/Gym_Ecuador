using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response;
using Asp.Versioning;
using LogicApi.Model.Response.Person;
using LogicApi.Model.Request.Person;

namespace GatewayCoreAPI.Controllers.V1;

/// <summary>
/// Constructor
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

