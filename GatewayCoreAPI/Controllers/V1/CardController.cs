using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Request.Card;
using LogicApi.Model.Response.Card;
using LogicApi.Model.Response;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CardController : SecurityControllerBase
{
     #region Constructor

     /// <summary>
     /// Constructor
     /// </summary>
     public CardController(
         IUserMessages userMessages,
         ILogger<CardController> logger,
         IMediator mediator
         ) : base(
             userMessages,
             logger,
             mediator)
     {
     }

     #endregion

     #region Methods Controller

     /// <summary>
     /// Registra una tarjeta
     /// </summary>
     /// <param name="request">Request de Registra una tarjeta</param>
     /// <returns></returns>
     [HttpPost("Register")]
     [ProducesResponseType(200, Type = typeof(GenericResponse<HandlerResponse>))]
     [ProducesResponseType(400, Type = typeof(GenericResponse))]
     public async Task<IActionResult> RegisterCard([FromBody] RegisterCardRequest request)
          => Success(await Mediator.Send(request).ConfigureAwait(false));

     /// <summary>
     /// Obtienes las tarjetas relacionadas al usuario
     /// </summary>
     /// <param name="request">Request de Obtienes las tarjetas relacionadas al usuario</param>
     /// <returns></returns>
     [HttpGet("MyCards")]
     [ProducesResponseType(200, Type = typeof(GenericResponse<GetMyCardsResponse>))]
     [ProducesResponseType(400, Type = typeof(GenericResponse))]
     public async Task<IActionResult> GetMyCards([FromQuery] GetMyCardsRequest request)
          => Success(await Mediator.Send(request).ConfigureAwait(false));
     /// <summary>
     /// Elimina la tarjeta asociada al usuario
     /// </summary>
     /// <param name="request">Request de Elimina la tarjeta asociada al usuario</param>
     /// <returns></returns>
     [HttpDelete]
     [ProducesResponseType(200, Type = typeof(GenericResponse<HandlerResponse>))]
     [ProducesResponseType(400, Type = typeof(GenericResponse))]
     public async Task<IActionResult> DeleteCard([FromBody] DeleteCardRequest request)
          => Success(await Mediator.Send(request).ConfigureAwait(false));

     /// <summary>
     /// Validación de Tarjeta
     /// </summary>
     /// <param name="request">Request de validación de Tarjeta</param>
     /// <returns></returns>
     [HttpPost("Validate")]
     [ProducesResponseType(200, Type = typeof(GenericResponse<ValidateCardResponse>))]
     [ProducesResponseType(400, Type = typeof(GenericResponse))]
     public async Task<IActionResult> ValidateCard([FromBody] ValidateCardRequest request)
          => Success(await Mediator.Send(request).ConfigureAwait(false));


     #endregion
}