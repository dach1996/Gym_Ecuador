using LogicApi.Model.Request.Authorization;
namespace LogicApi.BusinessLogic.AuthorizationHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class LogoutHandler(
    ILogger<LogoutHandler> logger,
    IPluginFactory pluginFactory) : AuthorizationBase<LogoutRequest, HandlerResponse>(
        logger,
        pluginFactory)
{
    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<HandlerResponse> Handle(LogoutRequest request, CancellationToken cancellationToken)
     => await ExecuteHandlerAsync(OperationApiName.Logout, request, async () =>
        {
            //Registra el Token en cache 
            await RegisterUsedTokenLogOut(request.ContextRequest.Headers.Authorization).ConfigureAwait(false);
            //Envía la respuesta
            return HandlerResponse.Complete();
        });


}
