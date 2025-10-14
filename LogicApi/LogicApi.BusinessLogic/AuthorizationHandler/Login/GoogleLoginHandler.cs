using System.Linq.Expressions;
using LogicApi.Abstractions.Interfaces.Authorization;
using LogicApi.Model.Implementations.Authorization;
using LogicApi.Model.Request.Authorization;
using LogicApi.Model.Response.Authorization;
using PersistenceDb.Models.Authentication;

namespace LogicApi.BusinessLogic.AuthorizationHandler.Login;
public class GoogleLoginHandler(
    ILogger<GoogleLoginHandler> logger,
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
      => await ExecuteHandlerAsync(OperationApiName.GoogleLogin, request, async () =>
      {
          //Valida que el usuario no esté vacío
          request.UserName.IsNullOrEmpty(new AuthException($"El corre no debe estar vacío"));
          //Valida que la contraseña no esté vacía
          request.Password.IsNullOrEmpty(new AuthException($"El token no puede estar vacio"));
          LoginRequest = request;
          //Valida el Logín con Google
          var authenticationResponse = await AuthenticationService.AuthenticateAsync(new(request.Password)).ConfigureAwait(false);
          var userId = await AdministratorCache.TryGetAsync<int?>(CacheCodes.UserIdByUserName(request.UserName)).ConfigureAwait(false);
          Expression<Func<User, bool>> where = userId.HasValue ? where => where.Id == userId : where => where.Email == request.UserName;
          var user = await UnitOfWork.UserRepository
              .GetFirstOrDefaultGenericAsync(
                  select => new
                  {
                      select.Id,
                      select.UserName,
                      select.IsBlocked,
                  },
                  where
                  ).ConfigureAwait(false);
          //Verifica si el usuario es null
          if (user is null)
          {
              //Registra el nuevo usuario y agrega el método manual
              await Mediator.Send(new CreateUserRequest
              {
                  Email = authenticationResponse.Email,
                  ConditionAndTermines = true,
                  LoginType = LoginImplementations.Google,
                  ContextRequest = ContextRequest
              }).ConfigureAwait(false);
              //Obtiene el primer usuario
              user = await UnitOfWork.UserRepository
              .GetFirstOrDefaultGenericAsync(
                  select => new
                  {
                      select.Id,
                      select.UserName,
                      select.IsBlocked,
                  },
                  where
                  ).ConfigureAwait(false);
          }
          if (!userId.HasValue)
              await AdministratorCache.SetAsync(CacheCodes.UserIdByUserName(request.UserName), user.Id, slidingExpiration: true).ConfigureAwait(false);
          ConfigureUserIdAndUserNameInContext(user.Id, user.UserName);
          //Valida que el usuario no esté bloqueado 
          if (user.IsBlocked)
              throw new CustomException((int)MessagesCodesError.UserBlocked);
          return await GetResponseAsync(user.Id).ConfigureAwait(false);
      }, true);
}
