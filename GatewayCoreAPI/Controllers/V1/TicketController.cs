using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response;
using LogicApi.Model.Request.Ticket;
using LogicApi.Model.Response.Ticket;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class TicketController : SecurityControllerBase
{
    /// <summary>
    /// Constructor
    /// </summary>
    public TicketController(
        IUserMessages userMessages,
        ILogger<TicketController> logger,
        IMediator mediator
        ) : base(
            userMessages,
            logger,
            mediator)
    {
    }


    #region Methods Controller

    /// <summary>
    /// Obtiene boletos disponibles
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("Search")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetTicketsAvailableResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse<HandlerResponse>))]
    public async Task<IActionResult> GetTickets([FromQuery] GetTicketsAvailableRequest request)
          => Success(await Mediator.Send(request).ConfigureAwait(false));
    #endregion
}