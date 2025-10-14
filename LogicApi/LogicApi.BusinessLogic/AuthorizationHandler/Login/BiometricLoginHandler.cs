using System.Linq.Expressions;
using LogicApi.Abstractions.Interfaces.Authorization;
using LogicApi.Model.Request.Authorization;
using LogicApi.Model.Response.Authorization;
using PersistenceDb.Models.Authentication;
namespace LogicApi.BusinessLogic.AuthorizationHandler.Login;

public class BiometricLoginHandler(
    ILogger<BiometricLoginHandler> logger,
    IPluginFactory pluginFactory) : LoginHandler(
        logger,
        pluginFactory), ILoginHandler
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<LoginResponse> Handle(LoginRequest request)
      => await ExecuteHandlerAsync(OperationApiName.BiometricLogin, request, async () =>
        {
            //Valida que el usuario no esté vacío
            request.UserName.IsNullOrEmpty(new AuthException($"El nombre de usuario no debe estar vacío"));
            LoginRequest = request;
            //Valida que exista el Usuario 
            var userId = await AdministratorCache.TryGetAsync<int?>(CacheCodes.UserIdByUserName(request.UserName)).ConfigureAwait(false);
            Expression<Func<User, bool>> where = userId.HasValue
                ? where => where.Id == userId
                : where => where.UserName == request.UserName || where.Email == request.UserName;
            var user = (await UnitOfWork.UserRepository
               .GetFirstOrDefaultGenericAsync(
                   select => new
                   {
                       select.Id,
                       select.UserName,
                       select.IsBlocked,
                   },
                   where
                   ).ConfigureAwait(false))
               ?? throw new CustomException((int)MessagesCodesError.InfoUserNotFound, $"No se pudo encontrar información de Usuario");
            //Asigna el Id de usuario
            if (!userId.HasValue)
            {
                userId = user.Id;
                await AdministratorCache.SetAsync(CacheCodes.UserIdByUserName(request.UserName), user.Id, slidingExpiration: true).ConfigureAwait(false);
            }
            //Obtiene el Id de dispositivo
            var deviceId = await AdministratorCache.TryGetAsync<int?>(CacheCodes.DeviceIdByMobileId(ContextRequest.Headers.DeviceId)).ConfigureAwait(false);
            if (!deviceId.HasValue)
            {
                deviceId = await UnitOfWork.DeviceRepository
                .GetFirstOrDefaultGenericAsync<int?>(
                    select => select.Id,
                    where => where.MobileId == ContextRequest.Headers.DeviceId
                ).ConfigureAwait(false)
                ?? throw new CustomException((int)MessagesCodesError.DeviceInfoNotFound, $"No existe registro de dispositivo con MobileId: {MobileId}");
                await AdministratorCache.SetAsync(CacheCodes.DeviceIdByMobileId(ContextRequest.Headers.DeviceId), deviceId, slidingExpiration: true).ConfigureAwait(false);
            }
            //Obtiene el registro de biométrico
            var biometric = await UnitOfWork.UserDeviceRepository
                .GetFirstOrDefaultGenericAsync(
                    select => select.Biometric,
                    where => where.UserId == userId && where.DeviceId == deviceId
                ).ConfigureAwait(false)
                ?? throw new CustomException((int)MessagesCodesError.DeviceInfoNotFound, $"No existe registro de dispositivo con MobileId: {MobileId}");
            ConfigureUserIdAndUserNameInContext(user.Id, user.UserName);
            //Valida que el usuario no esté bloqueado 
            if (user.IsBlocked)
                throw new CustomException((int)MessagesCodesError.UserBlocked);
            //Verifica la contraseña
            if (!biometric.Equals(request.Password.ToSha256(), StringComparison.CurrentCultureIgnoreCase))//validar el biométrico
            {
                await LoginFailedRegisterAsync(user.Id).ConfigureAwait(false);
                throw new CustomException((int)MessagesCodesError.BiometricIncorrect, $"Biométrico Incorrecto");
            }
            return await GetResponseAsync(user.Id).ConfigureAwait(false);
        }, true);

}
