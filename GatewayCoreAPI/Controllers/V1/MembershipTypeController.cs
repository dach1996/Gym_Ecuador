using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.MembershipType;
using LogicApi.Model.Request.MembershipType;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class MembershipTypeController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public MembershipTypeController(
        IUserMessages userMessages,
        ILogger<MembershipTypeController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Obtiene la lista de tipos de membresía con paginación
    /// </summary>
    /// <param name="request">Modelo para obtener tipos de membresía</param>
    /// <returns></returns>
    [HttpGet("GetMembershipTypes")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetMembershipTypesResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetMembershipTypes([FromQuery] GetMembershipTypesRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Crea un nuevo tipo de membresía
    /// </summary>
    /// <param name="request">Modelo para crear tipo de membresía</param>
    /// <returns></returns>
    [HttpPost("CreateMembershipType")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateMembershipTypeResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateMembershipType([FromBody] CreateMembershipTypeRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
