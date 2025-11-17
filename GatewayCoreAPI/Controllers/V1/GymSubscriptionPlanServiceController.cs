using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Request.GymSubscriptionPlanService;
using LogicApi.Model.Response.GymSubscriptionPlanService;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class GymSubscriptionPlanServiceController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public GymSubscriptionPlanServiceController(
        IUserMessages userMessages,
        ILogger<GymSubscriptionPlanServiceController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Asigna un servicio a un plan de suscripción
    /// </summary>
    /// <param name="request">Modelo para asignar servicio</param>
    /// <returns></returns>
    [HttpPost("AssignService")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<AssignServiceToPlanResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> AssignServiceToPlan([FromBody] AssignServiceToPlanRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Remueve un servicio de un plan de suscripción
    /// </summary>
    /// <param name="request">Modelo para remover servicio</param>
    /// <returns></returns>
    [HttpDelete("RemoveService")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<RemoveServiceFromPlanResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> RemoveServiceFromPlan([FromBody] RemoveServiceFromPlanRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene los servicios de un plan de suscripción
    /// </summary>
    /// <param name="request">Modelo para obtener servicios por plan</param>
    /// <returns></returns>
    [HttpGet("GetServicesByPlan")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetServicesByPlanResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetServicesByPlan([FromQuery] GetServicesByPlanRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}

