using Common.WebCommon.Models;
using LogicAdministratorApi.Model.CommonRecords;
using LogicAdministratorApi.Model.Request.Authorization;
using LogicAdministratorApi.Model.Response.Authorization;
namespace LogicAdministratorApi.BusinessLogic.AuthorizationHandler;
/// <summary>
/// Constructor
/// </summary>
public class LoginHandler : AuthorizationBase<LoginRequest, LoginResponse>
{
    protected readonly string AesSecret;

    /// <param name="logger"></param>
    /// <param name="pluginFactory"></param>
    public LoginHandler(
        ILogger<LoginHandler> logger,
        IPluginFactory pluginFactory) : base(
            logger,
            pluginFactory) => AesSecret = "";



    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.Login, request, async () =>
        {
            //Respondemos
            return new LoginResponse
            {
                EstablishmentAllowedItems = [],
                AccessSecret = "",
            };
        }, false);

}
