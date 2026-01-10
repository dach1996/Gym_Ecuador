using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Models;
using Common.WebApi.Controller;
using MediatR;
using LogicApi.Model.Response.SeriesRecord;
using LogicApi.Model.Request.SeriesRecord;
using Asp.Versioning;

namespace GatewayCoreAPI.Controllers.V1;

/// <summary>
/// Controlador de Registro de Series
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class SeriesRecordController(
    IUserMessages userMessages,
    ILogger<SeriesRecordController> logger,
    IMediator mediator) : SecurityControllerBase(
        userMessages,
        logger,
        mediator)
{
    #region Methods Controller

    /// <summary>
    /// Obtiene el historial de registros de series con paginación
    /// </summary>
    /// <param name="request">Modelo para obtener historial de series</param>
    /// <returns></returns>
    [HttpGet("GetSeriesRecordsHistory")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<GetSeriesRecordsHistoryResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> GetSeriesRecordsHistory([FromQuery] GetSeriesRecordsHistoryRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    /// <summary>
    /// Crea un nuevo registro de serie
    /// </summary>
    /// <param name="request">Modelo para crear registro de serie</param>
    /// <returns></returns>
    [HttpPost("CreateSeriesRecord")]
    [ProducesResponseType(200, Type = typeof(GenericResponse<CreateSeriesRecordResponse>))]
    [ProducesResponseType(400, Type = typeof(GenericResponse))]
    public async Task<IActionResult> CreateSeriesRecord([FromBody] CreateSeriesRecordRequest request)
        => Success(await Mediator.Send(request).ConfigureAwait(false));

    #endregion
}
