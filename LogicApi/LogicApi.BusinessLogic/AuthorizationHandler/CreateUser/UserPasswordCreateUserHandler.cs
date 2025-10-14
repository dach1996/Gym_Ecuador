using Common.Queue.Model.Template;
using LogicApi.Abstractions.Interfaces.Authorization;
using LogicApi.Model.Request.Authorization;
using LogicApi.Model.Response.Authorization;
using PersistenceDb.Models.Authentication;
namespace LogicApi.BusinessLogic.AuthorizationHandler.CreateUser;
public class UserPasswordCreateUserHandler(
    ILogger<UserPasswordCreateUserHandler> logger,
    IPluginFactory pluginFactory) : CreateUserHandler(
        logger,
        pluginFactory), ICreateUserHandler
{
    public async Task<CreateUserResponse> Handle(CreateUserRequest request)
     => await ExecuteHandlerAsync(OperationApiName.CreateUser, request, async () =>
        {
            //Genera nueva contraseña
            var newPassword = GeneratePassword();
            var newUser = await GetUserValidatedAsync(request, newPassword).ConfigureAwait(false);
            var passwordEncrypted = GetPasswordEncrypted(newPassword, newUser.Salt);
            newUser.ManualUserRegistrationForm.Password = passwordEncrypted;
            newUser.ManualUserRegistrationForm.PasswordTemporary = passwordEncrypted;
            _ = await AuthenticationUnitOfWork.UserRepository.UpdateAsync(newUser).ConfigureAwait(false);
            await SendQueueMessageAsync(new NewUserMailQueueTemplate
            {
                User = request.Email,
                Password = newPassword,
                To = newUser.Email.ToListElements()
            }).ConfigureAwait(false);
            //Configura el contexto
            ContextRequest.CustomClaims.ConfigureUser(newUser.Id, newUser.UserName);
            //Responde la respuesta
            return new CreateUserResponse
            {
                ShowMessage = true,
                UserMessage = GetSuccessMessage(MessagesCodesSucess.UserCreateSuccess),
                TemporalPassword = newPassword
            };
        }, UnitOfWorkType.Authentication, true);


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
            }
        ];
}
