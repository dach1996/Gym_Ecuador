using LogicApi.Model.Request.Authorization;
using LogicApi.Model.Request.CommonConfiguration;
using LogicApi.Model.Request.Security;
using LogicApi.Model.Response.Authorization;
namespace LogicApi.BusinessLogic.AuthorizationHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class RefreshTokenHandler(
    ILogger<RefreshTokenHandler> logger,
    IPluginFactory pluginFactory) : AuthorizationBase<RefreshTokenRequest, RefreshTokenResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    => await ExecuteHandlerAsync(OperationApiName.RefreshToken, request, async () =>
        {
            var maxCountRefreshToken = (await Mediator.Send(new GetParameterByCodeRequest(ParametersCodes.MaxCountRefreshToken, ContextRequest), cancellationToken).ConfigureAwait(false))
                          ?.IntValue;
            //Validar la cantidad máxima de refresh
            if (request.ContextRequest.CountRefresh > maxCountRefreshToken)
                throw new CustomException((int)MessagesCodesError.MaxRefreshToken, $"Se alcanzó un máximo de Refresh Token");
            var newCount = request.ContextRequest.CountRefresh + 1;
            //Genera el Jwt
            var userIdEncrypted = (await Mediator.Send(new EncryptRequest($"{ContextRequest.CustomClaims.UserId}", ContextRequest, false)).ConfigureAwait(false))
                ?.DictionaryEncrypted?.FirstOrDefault().Value;
            //Arma los claims
            var claims = new Dictionary<string, string>
             {
                 //Identificador del JWT
                 { "jti", Guid.NewGuid().ToString() },
                  //Nombre de Usuario
                 {$"{nameof(ContextRequest.CustomClaims.UserId)}", userIdEncrypted},
                  //RefreshToken
                 {$"{nameof(ContextRequest.CustomClaims.Refresh)}", $"{newCount}"},
             };
            var jwt = JwtManager.RefreshJwt(claims);
            return new RefreshTokenResponse(jwt.Token);
        });
}
