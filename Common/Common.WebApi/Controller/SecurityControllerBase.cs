using Common.Messages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
namespace Common.WebApi.Controller;
[Authorize]
public class SecurityControllerBase : ApiControllerBase
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    protected SecurityControllerBase(
        IUserMessages userMessages,
        ILogger<SecurityControllerBase> logger,
        IMediator mediator)
        : base(userMessages,
               logger,
               mediator)
    {
    }
}