using Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.GymReview;
using LogicApi.Model.Request.GymReview;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class GymReviewController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public GymReviewController(
        IUserMessages userMessages,
        ILogger<GymReviewController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Obtiene las reseñas de un gimnasio
    /// </summary>
    /// <param name="gymGuid">GUID del gimnasio</param>
    /// <param name="request">Parámetros de consulta</param>
    /// <returns></returns>
    [HttpGet("GetGymReviews/{gymGuid}")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetGymReviewsResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetGymReviews([FromRoute] Guid gymGuid, [FromQuery] GetGymReviewsRequest request)
    {
        request.GymGuid = gymGuid;
        return Success(await Mediator.Send(request).ConfigureAwait(false));
    }

    /// <summary>
    /// Crea una nueva reseña de gimnasio
    /// </summary>
    /// <param name="request">Modelo para crear reseña</param>
    /// <returns></returns>
    [HttpPost("CreateGymReview")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateGymReviewResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateGymReview([FromBody] CreateGymReviewRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
