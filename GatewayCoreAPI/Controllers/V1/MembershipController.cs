using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.Membership;
using LogicApi.Model.Request.Membership;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class MembershipController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public MembershipController(
        IUserMessages userMessages,
        ILogger<MembershipController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Obtiene la lista de membresías con paginación
    /// </summary>
    /// <param name="request">Modelo para obtener membresías</param>
    /// <returns></returns>
    [HttpGet("GetMemberships")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetMembershipsResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetMemberships([FromQuery] GetMembershipsRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene una membresía por su GUID
    /// </summary>
    /// <param name="membershipGuid">GUID de la membresía</param>
    /// <returns></returns>
    [HttpGet("GetMembershipByGuid/{membershipGuid}")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetMembershipByGuidResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetMembershipByGuid([FromRoute] Guid membershipGuid)
    {
        var request = new GetMembershipByGuidRequest { MembershipGuid = membershipGuid };
        return Success(await Mediator.Send(request).ConfigureAwait(false));
    }

    /// <summary>
    /// Crea una nueva membresía
    /// </summary>
    /// <param name="request">Modelo para crear membresía</param>
    /// <returns></returns>
    [HttpPost("CreateMembership")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateMembershipResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateMembership([FromBody] CreateMembershipRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Actualiza una membresía existente
    /// </summary>
    /// <param name="request">Modelo para actualizar membresía</param>
    /// <returns></returns>
    [HttpPut("UpdateMembership")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<UpdateMembershipResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> UpdateMembership([FromBody] UpdateMembershipRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
