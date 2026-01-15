using Common.Utils.Cryptography.Argon2;
using Common.WebApi.Models.EncryptedClaims;
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
            //Valida que la contraseña no esté vacía
            var platforms = await GetPlatformsAsync().ConfigureAwait(false);
            var platformId = platforms.FirstOrDefault(where => where.Code == RolePlatformType.Web.GetEnumMember())?.Id;
            var user = (await UnitOfWork.UserRepository
                .GetFirstOrDefaultGenericAsync(
                    select => new
                    {
                        select.Id,
                        select.UserName,
                        select.Guid,
                        select.Email,
                        select.IsBlocked,
                        PersonName = select.Person.Name + " " + select.Person.LastName,
                        select.Salt,
                        ManualRegister = select.UserRegistrationForms
                            .Where(where => where.UserTypeRegister == UserTypeRegister.Manual)
                            .Select(select => new
                            {
                                select.Password,
                                select.PasswordTemporary
                            }).FirstOrDefault(),
                        Roles = select.UserRoleScopes
                        .Where(where => where.Role.PlatformId == platformId)
                        .Select(select => new
                        {
                            select.ScopeIdentifier,
                            select.Role.Name,
                            select.Role.Scope,
                            select.Role.Id,
                            Functionalities = select.Role.RoleFunctionalities.Select(functionality => new
                            {
                                functionality.Functionality.Id,
                                FunctionalityCode = functionality.Functionality.Code,
                                functionality.Functionality.Function.Code,
                                functionality.Functionality.Function.Route,
                                functionality.Functionality.Function.Icon,
                                functionality.Functionality.Function.Order,
                                functionality.Functionality.Function.Name,
                            }).ToList()
                        }).ToList()
                    },
                    where => where.UserName == request.Username || where.Email == request.Username
                    ).ConfigureAwait(false))
                ?? throw new CustomException((int)MessagesCodesError.InfoUserNotFound, $"No se pudo encontrar información de Usuario");
            //Valida que el usuario no esté bloqueado 
            if (user.IsBlocked)
                throw new CustomException((int)MessagesCodesError.UserBlocked);
            //Forma manual de registro
            var manualFormRegister = user.ManualRegister
              ?? throw new CustomException((int)MessagesCodesError.InfoUserNotFound, $"No se pudo encontrar información de Usuario");
            //Verifica la contraseña
            if (!Argon2.VerifyHashes(request.Password, [manualFormRegister.Password, manualFormRegister.PasswordTemporary ?? string.Empty], user.Salt))
                throw new CustomException((int)MessagesCodesError.InfoUserNotFound, $"Contraseña Incorrecta.");
            var jwt = await GenerateJwtAsync(new EncryptedFieldsClaimsAdministrator
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserGuid = user.Guid,
                RoleScopeClaims = [.. user.Roles
                .Select(select => new RoleScopeClaim
                {
                    Identifier = select.ScopeIdentifier,
                    Scope = (byte)select.Scope,
                    RoleId = select.Id,
                })]
            }).ConfigureAwait(false);
            //Respondemos con el token
            return new LoginResponse
            {
                AccessToken = jwt.Token,
                Username = user.UserName,
                Email = user.Email,
                RoleName = user.Roles.FirstOrDefault()?.Name,
                PersonName = user.PersonName,
                MenuItems = [.. user.Roles.SelectMany(select => select.Functionalities)
                .GroupBy(select => select.Code)
                .Select(select => select.FirstOrDefault())
                .OrderBy(select => select.Order)
                .Select(select => new MenuItem
                {
                    Name = select.Name,
                    Route = select.Route,
                    Icon = select.Icon,
                })]
            };
        }, false);

    /// <summary>
    /// Genera el Jwt
    /// </summary>
    /// <param name="user"></param>
    /// <param name="deviceLoginData"></param>
    /// <returns></returns>
    private async Task<JsonWebTokenModel> GenerateJwtAsync(EncryptedFieldsClaimsAdministrator encryptedFieldClaim)
    {
        //Arma los claims
        var encryptedFieldClaimJson = encryptedFieldClaim.ToJson().EncryptAes(AesSecret);
        var claims = new Dictionary<string, string>
             {
                 //Identificador del JWT
                 { "jti", Guid.NewGuid().ToString() },
                  //Nombre de Usuario
                 {$"{nameof(EncryptedFieldsClaimsAdministrator)}", encryptedFieldClaimJson},
                  //RefreshToken
                 {$"{nameof(ContextRequest.CustomClaims.Refresh)}", $"{1}"},
             };
        //Arma el Token
        var jwt = JwtManager.BuildJwt(claims);
        return await Task.FromResult(jwt).ConfigureAwait(false);
    }

}
