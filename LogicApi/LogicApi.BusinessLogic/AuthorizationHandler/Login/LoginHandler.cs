using Common.WebApi.Models.EncryptedClaims;
using Common.WebCommon.Models;
using LogicApi.Abstractions.Interfaces.Authorization;
using LogicCommon.Model.Request.Administration;
using LogicApi.Model.Request.Authorization;
using LogicApi.Model.Request.Device;
using LogicApi.Model.Response.Authorization;
using PersistenceDb.Models.Authentication;
namespace LogicApi.BusinessLogic.AuthorizationHandler.Login;

public class LoginHandler(
    ILogger<LoginHandler> logger,
    IPluginFactory pluginFactory) : AuthorizationBase<LoginRequest, LoginResponse>(
        logger,
        pluginFactory)
{
    protected LoginRequest LoginRequest;
    protected int CurrentLoginUserId;
    protected bool ForceChangePassword;

    protected string AesSecret => AppSettingsApi.AesConfiguration.Keys.FirstOrDefaultValue(where => where.Key == AesConfiguration.AesImplementationName.ServerGeneral);

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken) =>
         await PluginFactory.GetPlugin<ILoginHandler>($"{request.LoginType.ToString().ToUpper()}")
            .Handle(request).ConfigureAwait(false);

    /// <summary>
    /// Obtiene la respuesta
    /// </summary>
    /// <param name="outputSwitch"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    protected async Task<LoginResponse> GetResponseAsync(int userId)
    {
        //Verifica si la persona no ha sido asignada
        var user = await UnitOfWork.UserRepository.GetFirstOrDefaultGenericAsync(
            select => new
            {
                select.Email,
                select.Phone,
                select.UserName,
                select.HasCompleteRegistration,
                select.Guid,
                select.LanguageCode,
                select.FirstLoginDate,
                select.ImagenId,
                select.HasVerifiedData,
                Person = select.PersonId.HasValue ? new
                {
                    select.Person.Id,
                    select.Person.Name,
                    select.Person.LastName,
                    select.Person.RealNames,
                    select.Person.RealLastNames,
                    select.Person.DocumentNumber,
                    IdentificationType = select.Person.TypeIdentification.Name
                } : null
            },
            where => where.Id == userId
        ).ConfigureAwait(false);
        if (user.Person is null)
            throw new CustomException((int)MessagesCodesError.UserHasNotAssignedPerson, $"El usuario con Id: '{userId}' no posee asignado una persona.");
        LoginResponse loginResponse = new();
        //Actualia la información de Dipositivos y Usuario
        var deviceId = await UpdateDataUserDeviceAsync(userId, user.FirstLoginDate).ConfigureAwait(false);
        //Registra el push token
        if (!LoginRequest.PushToken.IsNullOrEmpty())
            await Mediator.Send(new RegisterPushTokenRequest(ContextRequest, LoginRequest.PushToken)).ConfigureAwait(false);
        //Obtiene el Token del Usuario
        loginResponse.AccessToken = (await GenerateJwtAsync(
            new EncryptedFieldsApi
            {
                UserId = userId,
                UserName = user.UserName,
                DeviceId = deviceId,
                MobileId = ContextRequest.Headers.DeviceId,
                UserGuid = user.Guid,
                PersonId = user.Person.Id
            }
         ).ConfigureAwait(false))?.Token;
        //Información de Cooperativas
        loginResponse.InfoUser = new UserInfo
        {
            DocumentNumber = user.Person.DocumentNumber,
            Email = user.Email,
            FirstName = user.Person.Name ?? user.Person.RealNames.Split(' ').FirstOrDefault(),
            Surname = user.Person.LastName ?? user.Person.RealLastNames.Split(' ').FirstOrDefault(),
            PhoneNumber = user.Phone,
            IdentificationType = user.Person.IdentificationType,
            Username = user.HasCompleteRegistration ? user.UserName : user.Email,
            GuidIdentifier = user.Guid,
            HasVerifiedData = user.HasVerifiedData,
            HasAnyProcessTracking = await UnitOfWork.ProcessTrackingRepository.ExistAnyAsync(where => where.UserId == userId).ConfigureAwait(false)
        };
        //Obtiene la imagen si existe
        if (user.ImagenId.HasValue)
            loginResponse.InfoUser.UrlImage = await GetUrlImageByCacheAsync(user.ImagenId.Value).ConfigureAwait(false);
        //Tarea para Obtener los Catálogos Iniciales
        loginResponse.GetInitialCataloguesResponse = await Mediator.Send(new GetInitialCataloguesCommonRequest(ContextRequest)).ConfigureAwait(false);
        loginResponse.Configuration = new UserConfiguration
        {
            Language = user.LanguageCode,
            ForceChangePassword = ForceChangePassword
        };
        loginResponse.MaxCalification = 5;
        loginResponse.FilterConfiguration = new()
        {
            GymTypes =
            [
                new ("Pesas", "Pesas"),
                new ("Crossfit", "Crossfit"),
                new ("Boxeo", "Boxeo"),
                new ("Yoga", "Yoga"),
                new ("Pilates", "Pilates"),
                new ("Piscina", "Piscina"),
                new ("Sauna", "Sauna"),
                new ("Nutricionista", "Nutricionista"),
                new ("Parking", "Parking"),
                new ("Clases", "Clases")

            ],
            Services =
            [
                new("Piscina", "Piscina"),
                new("Sauna", "Sauna"),
                new("Nutricionista", "Nutricionista"),
                new("Parking", "Parking"),
                new("Clases", "Clases")
            ]
        };
        return loginResponse;
    }

   

    /// <summary>
    /// Actualiza la Información del Dipositivo y Usuario
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private async Task<int> UpdateDataUserDeviceAsync(int userId, DateTime? firstLoginDate)
    {
        var now = Clock.Now();
        //Registra o actualiza datos del dispositivo
        var deviceId = await AdministratorCache.TryGetAsync<int?>(CacheCodes.DeviceIdByMobileId(ContextRequest.Headers.DeviceId)).ConfigureAwait(false);
        if (!deviceId.HasValue)
        {
            deviceId = await UnitOfWork.DeviceRepository.GetFirstOrDefaultGenericAsync<int?>(
                    select => select.Id,
                    where => where.MobileId == ContextRequest.Headers.DeviceId
                 ).ConfigureAwait(false)
            ?? (await UnitOfWork.DeviceRepository.AddAsync(new Device
            {
                Brand = ContextRequest.Headers.Brand,
                MobileId = ContextRequest.Headers.DeviceId,
                Model = ContextRequest.Headers.Model,
                Platform = (PlatformType)(int)ContextRequest.Headers.Platform,
                SystemOperation = ContextRequest.Headers.SystemOperation ?? string.Empty,
                HasGoogleServices = ContextRequest.Headers.HasGoogleServices
            }).ConfigureAwait(false)).Id;
            await AdministratorCache.SetAsync(CacheCodes.DeviceIdByMobileId(ContextRequest.Headers.DeviceId), deviceId.Value, slidingExpiration: true).ConfigureAwait(false);
        }
        //Intenta actualizar el registro de usuario dispositivo
        var updateUserDevice = await UnitOfWork.UserDeviceRepository.UpdateByAsync(
            (userDevice => userDevice.DateTimeLastLogin, now),
            where => where.DeviceId == deviceId && where.UserId == userId).ConfigureAwait(false);
        //Si no se actualizó, se crea el registro
        if (updateUserDevice == 0)
            await UnitOfWork.UserDeviceRepository.AddAsync(new UserDevice
            {
                RegisterDate = now,
                DateTimeLastLogin = now,
                DeviceId = deviceId.Value,
                UserId = userId,
            }).ConfigureAwait(false);

        //Actualiza la información del usuario
        await UnitOfWork.UserRepository.UpdateByAsync(
            (user => user.FirstLoginDate, firstLoginDate ?? now),
            (user => user.DateTimeTriedLoginFailed, null),
            (user => user.TriedLoginFailed, 0),
            where => where.Id == userId).ConfigureAwait(false);
        return deviceId.Value;
    }

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

    /// <summary>
    /// Registrar el Logín fallido
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    protected async Task LoginFailedRegisterAsync(int userId)
    {
        //Obtiene el máximo de tokens a regenerar
        var maxAttemptsLoginFailed = await GetIntParameterAsync(ParametersCodes.MaxAttemptsLoginFailed).ConfigureAwait(false);
        //Obtiene el usuario
        var userTriedLoginFailed = await UnitOfWork.UserRepository.GetFirstOrDefaultGenericAsync(
            select => select.TriedLoginFailed,
            where => where.Id == userId).ConfigureAwait(false);
        var isBlocked = (userTriedLoginFailed ?? 0) >= maxAttemptsLoginFailed;
        //Verifica si el usuario está bloqueado
        await UnitOfWork.UserRepository.UpdateByAsync(
            (user => user.DateTimeTriedLoginFailed, Clock.Now()),
            (user => user.TriedLoginFailed, (userTriedLoginFailed ?? 0) + 1),
            (user => user.IsBlocked, isBlocked),
            where => where.Id == userId).ConfigureAwait(false);
    }
}