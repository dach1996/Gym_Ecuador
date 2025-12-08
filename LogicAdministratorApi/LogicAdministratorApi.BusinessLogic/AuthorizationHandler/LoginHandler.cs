using Common.WebCommon.Models;
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
            pluginFactory) => AesSecret = AppSettingsAdministrator.AesConfiguration.Keys.GetFirstOrDefaultValue(AesConfiguration.AesImplementationName.ServerGeneral);



    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.Login, request, async () =>
        {

            var jwt = await GenerateJwtAsync(new EncryptedFieldClaimCommon
            {
                UserName = "test",
                Email = "test@test.com",
            }).ConfigureAwait(false);
            //Respondemos con el token
            return new LoginResponse
            {
                AccessSecret = jwt.Token,
            };
        }, false);

    /// <summary>
    /// Genera el Jwt
    /// </summary>
    /// <param name="user"></param>
    /// <param name="deviceLoginData"></param>
    /// <returns></returns>
    private async Task<JsonWebTokenModel> GenerateJwtAsync(EncryptedFieldClaimCommon encryptedFieldClaim)
    {
        //Arma los claims
        var encryptedFieldClaimJson = encryptedFieldClaim.ToJson().EncryptAes(AesSecret);
        var claims = new Dictionary<string, string>
             {
                 //Identificador del JWT
                 { "jti", Guid.NewGuid().ToString() },
                  //Nombre de Usuario
                 {$"{nameof(EncryptedFieldClaimCommon)}", encryptedFieldClaimJson},
                  //RefreshToken
                 {$"{nameof(ContextRequest.CustomClaims.Refresh)}", $"{1}"},
             };
        //Arma el Token
        var jwt = JwtManager.BuildJwt(claims);
        return await Task.FromResult(jwt).ConfigureAwait(false);
    }

}
