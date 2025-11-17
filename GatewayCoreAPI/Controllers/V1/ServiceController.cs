using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Request.Service;
using LogicApi.Model.Response.Service;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ServiceController : SecurityControllerBase
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public ServiceController(
        IUserMessages userMessages,
        ILogger<ServiceController> logger,
        IMediator mediator) : base(
            userMessages,
            logger,
            mediator)
    {
    }

    #endregion

    #region Methods Controller

    /// <summary>
    /// Crea un nuevo servicio
    /// </summary>
    /// <param name="request">Modelo para crear servicio</param>
    /// <returns></returns>
    [HttpPost("Create")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateServiceResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateService([FromBody] CreateServiceRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Actualiza un servicio
    /// </summary>
    /// <param name="request">Modelo para actualizar servicio</param>
    /// <returns></returns>
    [HttpPut("Update")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<UpdateServiceResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> UpdateService([FromBody] UpdateServiceRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene la lista de servicios con paginación
    /// </summary>
    /// <param name="request">Modelo para obtener servicios</param>
    /// <returns></returns>
    [HttpGet("GetServices")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetServicesResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetServices([FromQuery] GetServicesRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Obtiene un servicio por su ID
    /// </summary>
    /// <param name="request">Modelo para obtener servicio por ID</param>
    /// <returns></returns>
    [HttpGet("GetById")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetServiceByIdResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetServiceById([FromQuery] GetServiceByIdRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}

