using System.Linq.Expressions;
using Common.Utils.Cryptography.Argon2;
using LogicApi.Abstractions.Interfaces.Authorization;
using LogicApi.Model.Request.Authorization;
using LogicApi.Model.Response.Authorization;
using PersistenceDb.Models.Authentication;

namespace LogicApi.BusinessLogic.AuthorizationHandler.Login;

public class UserPasswordLoginHandler(
    ILogger<UserPasswordLoginHandler> logger,
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
     => await ExecuteHandlerAsync(OperationApiName.UserPasswordLogin, request, async () =>
        {
            //Valida que el usuario no esté vacío
            request.UserName.IsNullOrEmpty(new AuthException($"El nombre de usuario no debe estar vacío"));
            //Valida que la contraseña no esté vacía
            request.Password.IsNullOrEmpty(new AuthException($"La contraseña está vacío."));
            request.Password = await DecryptValueAsync(request.Password, true).ConfigureAwait(false);
            LoginRequest = request;
            //Busca el perfil del usuario en la Base de Datos, este dato puede ser null ya que puede ser el primer login del usuario
            var userId = await AdministratorCache.TryGetAsync<int?>(CacheCodes.UserIdByUserName(request.UserName)).ConfigureAwait(false);
            Expression<Func<User, bool>> where = userId.HasValue ? where => where.Id == userId : where => where.UserName == request.UserName || where.Email == request.UserName;
            var user = (await AuthenticationUnitOfWork.UserRepository
                .GetFirstOrDefaultGenericAsync(
                    select => new
                    {
                        select.Id,
                        select.UserName,
                        select.IsBlocked,
                        select.Salt,
                    },
                    where
                    ).ConfigureAwait(false))
                ?? throw new CustomException((int)MessagesCodesError.InfoUserNotFound, $"No se pudo encontrar información de Usuario");
            ConfigureUserIdAndUserNameInContext(user.Id, user.UserName);
            if (!userId.HasValue)
                await AdministratorCache.SetAsync(CacheCodes.UserIdByUserName(request.UserName), user.Id, slidingExpiration: true).ConfigureAwait(false);
            //Valida que el usuario no esté bloqueado 
            if (user.IsBlocked)
                throw new CustomException((int)MessagesCodesError.UserBlocked);
            //Forma manual de registro
            var manualFormRegister = (await AuthenticationUnitOfWork.UserRegistrationFormRepository
              .GetFirstOrDefaultGenericAsync(
                  select => new
                  {
                      select.Password,
                      select.PasswordTemporary,
                  },
                  where => where.UserId == user.Id && where.UserTypeRegister == UserTypeRegister.Manual
                  ).ConfigureAwait(false))
              ?? throw new CustomException((int)MessagesCodesError.InfoUserNotFound, $"No se pudo encontrar información de Usuario");
            ForceChangePassword = !manualFormRegister.PasswordTemporary?.IsNullOrEmpty() ?? false;
            //Verifica la contraseña
            if (!Argon2.VerifyHashes(request.Password, [manualFormRegister.Password, manualFormRegister.PasswordTemporary ?? string.Empty], user.Salt))
            {
                await LoginFailedRegisterAsync(user.Id).ConfigureAwait(false);
                throw new CustomException((int)MessagesCodesError.InfoUserNotFound, $"Contraseña Incorrecta.");
            }
            return await GetResponseAsync(user.Id).ConfigureAwait(false);
        }, [UnitOfWorkType.Authentication, UnitOfWorkType.Administration], true);
}