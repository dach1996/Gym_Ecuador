using Common.Messages;
using Common.WebApi.Models;
using Common.WebApi.Models.Enum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace Common.WebApi.Controller;

/// <summary>
/// Constructor
/// </summary>
/// <param name="userMessages"></param>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
public abstract class GenericControlerBase(
    IUserMessages userMessages,
    ILogger<GenericControlerBase> logger,
    IMediator mediator) : ControllerBase
{
    protected readonly IUserMessages UserMessages = userMessages;
    protected readonly ILogger Logger = logger;
    protected readonly IMediator Mediator = mediator;

    /// <summary>
    /// Respuesta correcta
    /// </summary>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected IActionResult Success<T>(T data)
    {
        var message = UserMessages.GetDefaultSucessMessage();
        return Ok(new GenericResponse<T>
        {
            Code = message.Code,
            ResponseType = nameof(ResponseType.Success),
            Message = message.Message,
            Content = data
        });
    }
}

