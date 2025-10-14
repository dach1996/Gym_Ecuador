using Common.Utils.Cryptography.Argon2;
using LogicApi.Model.Request.Authorization;
using LogicApi.Model.Response.Authorization;
using PersistenceDb.Models.Authentication;

namespace LogicApi.BusinessLogic.AuthorizationHandler.AssignPerson;
public class UserPasswordAssignPersonHandler(
    ILogger<UserPasswordAssignPersonHandler> logger,
    IPluginFactory pluginFactory) : AssignPersonHandler(
        logger,
        pluginFactory)
{
    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public override async Task<LoginResponse> Handle(AssignPersonRequest request)
         => await ExecuteHandlerAsync(OperationApiName.AssignPerson, request, async () =>
            await ExecuteLoginValidationsAsync(request).ConfigureAwait(false)
       , registerLogAudit: true);

    /// <summary>
    /// Obtiene el usuario
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override async Task<User> GetUserAsync(AssignPersonRequest request)
    {
        //Busca el usuario en base de datos
        var user = await UnitOfWork.UserRepository
                    .GetByFirstOrDefaultAsync(where =>
                        where.Email == request.Email,
                        include => include.Person,
                        include => include.UserRegistrationForms).ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.InfoUserNotFound, $"No se pudo encontrar información de Usuario con corre {request.Email}");
        //Si el usuario ya está asignado a una persona no puede continuar el procesa 
        if (user.Person is not null)
            throw new CustomException((int)MessagesCodesError.UserHasAssignedPerson, $"El usuario posee asignado a la persona : '{request.DocumentNumber}'");

        ConfigureUserIdAndUserNameInContext(user.Id, user.UserName);
        var userRegisterForm = user.ManualUserRegistrationForm;
        request.Password = await DecryptValueAsync(request.Password, true).ConfigureAwait(false);
        //Verifica la contraseña
        if (!Argon2.VerifyHashes(request.Password, [userRegisterForm.Password, userRegisterForm.PasswordTemporary ?? string.Empty], user.Salt))
            throw new CustomException((int)MessagesCodesError.InfoUserNotFound, $"Contraseña Incorrecta.");
        return user;
    }

    /// <summary>
    /// Imple,entaciòn Login
    /// </summary>
    /// <param name="request"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    protected override async Task<LoginResponse> GetLoginAsync(AssignPersonRequest request, User user)
    {
        return await Mediator.Send(new LoginRequest
        {
            UserName = user.UserName,
            Password = await EncryptedValueAsync(request.NewPassword, true).ConfigureAwait(false),
            LoginType = request.LoginType,
            ContextRequest = ContextRequest,
        }).ConfigureAwait(false);
    }
}
