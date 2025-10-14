using Common.Utils.Cryptography.Argon2;
using LogicApi.Abstractions.Interfaces.Authorization;
using LogicApi.Model.Request.Authorization;
using LogicApi.Model.Response.Authorization;
using PersistenceDb.Models.Authentication;
namespace LogicApi.BusinessLogic.AuthorizationHandler.CreateUser;
public abstract class CreateUserHandler(
    ILogger<CreateUserHandler> logger,
    IPluginFactory pluginFactory) : AuthorizationBase<CreateUserRequest, CreateUserResponse>(
        logger,
        pluginFactory)
{
    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken) =>
         await PluginFactory.GetPlugin<ICreateUserHandler>($"{request.LoginType.ToString().ToUpper()}")
            .Handle(request).ConfigureAwait(false);

    /// <summary>
    /// Obtiene el usuario validado
    /// </summary>
    /// <param name="request"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    protected async Task<User> GetUserValidatedAsync(
        CreateUserRequest request,
        string newPassword
    )
    {
        //Valida los términos de contrato
        if (!request.ConditionAndTermines)
            throw new CustomException((int)MessagesCodesError.TermsAndConditionNotAccept);
        //Buscamos el usuario
        var newUser = await UnitOfWork.UserRepository
        .GetByFirstOrDefaultAsync(
            where => where.Email == request.Email,
            include => include.UserRegistrationForms).ConfigureAwait(false);
        //Validaciones de usuario
        if (newUser is not null && newUser.HasCompleteRegistration && !newUser.Email.IsNullOrEmpty())
            throw new CustomException((int)MessagesCodesError.UserEmailExist);
        //Usuario
        if (newUser is null)
        {
            var salt = Argon2.GenerateRandomSecretBytes();
            var passwordEncrypted = GetPasswordEncrypted(newPassword, salt);
            newUser = new User
            {
                UserName = $"Temporal_{Guid.NewGuid().ToString().ToUpper()}",
                Email = request.Email,
                DateTimeRegister = Now,
                Guid = Guid.NewGuid(),
                LanguageCode = $"{ContextRequest.Headers.UserLanguage}",
                UserRegistrationForms = [.. GetUserRegistrationForms(passwordEncrypted)],
                Salt = salt
            };
            //Guarda el neuvo usuario
            _ = await UnitOfWork.UserRepository.AddAsync(newUser).ConfigureAwait(false);
        }
        return newUser;
    }

    /// <summary>
    /// Obtiene las formas de registro del usuario
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<UserRegistrationForm> GetUserRegistrationForms(string passwordEncrypted);
}
