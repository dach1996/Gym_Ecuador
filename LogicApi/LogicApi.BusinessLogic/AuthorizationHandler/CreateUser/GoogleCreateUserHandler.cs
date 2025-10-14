using Common.Utils.Cryptography.Argon2;
using LogicApi.Abstractions.Interfaces.Authorization;
using LogicApi.Model.Request.Authorization;
using LogicApi.Model.Response.Authorization;
using PersistenceDb.Models.Authentication;
namespace LogicApi.BusinessLogic.AuthorizationHandler.CreateUser;
public class GoogleCreateUserHandler(
    ILogger<GoogleCreateUserHandler> logger,
    IPluginFactory pluginFactory) : CreateUserHandler(
        logger,
        pluginFactory), ICreateUserHandler
{
    public async Task<CreateUserResponse> Handle(CreateUserRequest request)
     => await ExecuteHandlerAsync(OperationApiName.CreateUser, request, async () =>
     {
         //Genera nueva contraseña
         var newPasswordClear = GeneratePassword();
         User newUser = await GetUserValidatedAsync(request, newPasswordClear).ConfigureAwait(false);
         var passwordEncrypted = GetPasswordEncrypted(newPasswordClear, newUser.Salt);
         newUser.ManualUserRegistrationForm.Password = passwordEncrypted;
         newUser.ManualUserRegistrationForm.PasswordTemporary = passwordEncrypted;
         newUser.GoogleUserRegistrationForm.Password = passwordEncrypted;
         newUser.GoogleUserRegistrationForm.PasswordTemporary = passwordEncrypted;
         _ = await UnitOfWork.UserRepository.UpdateAsync(newUser).ConfigureAwait(false);
         //Configura el contexto
         ContextRequest.CustomClaims.ConfigureUser(newUser.Id, newUser.UserName);
         //Responde la respuesta
         return new CreateUserResponse
         {
             ShowMessage = true,
             UserMessage = GetSuccessMessage(MessagesCodesSucess.UserCreateSuccess),
             TemporalPassword = newPasswordClear
         };
     }, true);

    /// <summary>
    /// Obtiene las formas de registro del usuario
    /// </summary>
    /// <param name="passwordEncrypted"></param>
    /// <returns></returns>
    protected override IEnumerable<UserRegistrationForm> GetUserRegistrationForms(string passwordEncrypted) => [
            new()
            {
                DateTimeRegister = Now,
                DateTimeLastAccess = Now,
                UserTypeRegister = UserTypeRegister.Manual,
                Password = passwordEncrypted,
                PasswordTemporary = passwordEncrypted,
            },
            new()
            {
                DateTimeRegister = Now,
                DateTimeLastAccess = Now,
                UserTypeRegister = UserTypeRegister.Google,
                Password = passwordEncrypted,
                PasswordTemporary = passwordEncrypted,
            }
        ];


}