using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Request.GymSubscriptionPlan;
using LogicApi.Model.Response.GymSubscriptionPlan;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class GymSubscriptionPlanController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public GymSubscriptionPlanController(
        IUserMessages userMessages,
        ILogger<GymSubscriptionPlanController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Crea un nuevo plan de suscripción
    /// </summary>
    /// <param name="request">Modelo para crear plan de suscripción</param>
    /// <returns></returns>
    [HttpPost("Create")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateGymSubscriptionPlanResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateGymSubscriptionPlan([FromBody] CreateGymSubscriptionPlanRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Actualiza un plan de suscripción
    /// </summary>
    /// <param name="request">Modelo para actualizar plan de suscripción</param>
    /// <returns></returns>
    [HttpPut("Update")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<UpdateGymSubscriptionPlanResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> UpdateGymSubscriptionPlan([FromBody] UpdateGymSubscriptionPlanRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene la lista de planes de suscripción con paginación
    /// </summary>
    /// <param name="request">Modelo para obtener planes de suscripción</param>
    /// <returns></returns>
    [HttpGet("GetPlans")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetGymSubscriptionPlansResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetGymSubscriptionPlans([FromQuery] GetGymSubscriptionPlansRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene un plan de suscripción por su GUID
    /// </summary>
    /// <param name="request">Modelo para obtener plan por GUID</param>
    /// <returns></returns>
    [HttpGet("GetByGuid")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetGymSubscriptionPlanByGuidResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetGymSubscriptionPlanByGuid([FromQuery] GetGymSubscriptionPlanByGuidRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}

