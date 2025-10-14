using Common.Authentication;
using Common.Security.Interface;
using Common.Security.Model.Enum;
using Common.Utils.Cryptography.Argon2;
using PasswordGenerator;

namespace LogicApi.BusinessLogic.AuthorizationHandler;
public abstract class AuthorizationBase<TRequest, TResponse>(
    ILogger<AuthorizationBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    private IAuthenticationService _authenticationService;
    protected IAuthenticationService AuthenticationService => _authenticationService ??=
        PluginFactory.GetPlugin<IAuthenticationService>(AppSettingsApi.AuthenticationServiceConfiguration?.CurrentImplementation, true);
    protected IJwtManager JwtManager => PluginFactory.GetPlugin<IJwtManager>(JwtIdentifier.Mobile.ToString());


    /// <summary>
    /// Obtiene la contraseña encriptada
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    protected static string GetPasswordEncrypted(string password, string salt) => Argon2.GenerateHash(password, salt);


    /// <summary>
    /// Registra un Token utilizado
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    protected async Task RegisterUsedTokenLogOut(string token)
    {
        var expirationMinutes = AppSettingsApi?.JwtSettings?.FirstOrDefault()?.AccessExpiration
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "No se ha configurado un tiempo de expiración de Token");
        await AdministratorCache.SetAsync(CacheCodes.LogOutToken(token), string.Empty, expirationMinutes).ConfigureAwait(false);
    }

    /// <summary>
    /// Genera una contraseña
    /// </summary>
    /// <returns></returns>
    protected static string GeneratePassword() => new Password()
            .IncludeLowercase()
            .IncludeUppercase()
            .IncludeNumeric()
            .LengthRequired(10)
            .Next();
}
