using Common.Security.Interface;
using Common.Security.Model.Enum;

namespace LogicAdministratorApi.BusinessLogic.AuthorizationHandler;
public abstract class AuthorizationBase<TRequest, TResponse>(ILogger<AuthorizationBase<TRequest, TResponse>> logger, IPluginFactory pluginFactory)
: BusinessLogicAdministratorBase(logger, pluginFactory), IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    protected readonly IJwtManager JwtManager = pluginFactory.GetPlugin<IJwtManager>($"{JwtIdentifier.Web}");

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
