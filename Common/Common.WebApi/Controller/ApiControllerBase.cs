using Common.Messages;
using Common.WebApi.Attributes.ContextAttribute;
using Common.WebApi.Attributes.DecryptAttribute;
using Common.WebApi.Attributes.ValidateDevelopServices;
using Common.WebApi.Attributes.ValidateScope;
using Common.WebApi.Attributes.ValidateToken;
using Common.WebApi.Models;
using Common.WebApi.Models.Enum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace Common.WebApi.Controller;

[ServiceFilter(typeof(AddContextAttribute))]
[ServiceFilter(typeof(ValidateTokenAttribute))]
[ServiceFilter(typeof(DecryptFieldAttribute))]
[ServiceFilter(typeof(ValidateScopeAttribute))]
[ServiceFilter(typeof(ValidateDevelopServicesAttribute))]
public class ApiControllerBase : GenericControlerBase
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessages"></param>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    /// <returns></returns>
    public ApiControllerBase(
        IUserMessages userMessages,
        ILogger<ApiControllerBase> logger,
        IMediator mediator) : base(userMessages, logger, mediator)
    {
    }
}

