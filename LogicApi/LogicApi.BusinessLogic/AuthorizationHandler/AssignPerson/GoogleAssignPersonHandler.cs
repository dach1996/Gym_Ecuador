using LogicApi.Model.Request.Authorization;
using LogicApi.Model.Response.Authorization;
using PersistenceDb.Models.Authentication;

namespace LogicApi.BusinessLogic.AuthorizationHandler.AssignPerson;
public class GoogleAssignPersonHandler(
    ILogger<GoogleAssignPersonHandler> logger,
    IPluginFactory pluginFactory) : AssignPersonHandler(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public override  async Task<LoginResponse> Handle(AssignPersonRequest request)
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
        //Valida la authenticación
        var authenticationResponse = await AuthenticationService.AuthenticateAsync(new(request.Password)).ConfigureAwait(false);
        //Busca el usuario en base de datos
        var user = await UnitOfWork.UserRepository
                    .GetByFirstOrDefaultAsync(where =>
                        where.Email == authenticationResponse.Email,
                        include => include.Person,
                        include => include.UserRegistrationForms).ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.InfoUserNotFound, $"No se pudo encontrar información de Usuario");
        //Si el usuario ya está asignado a una persona no puede continuar el procesa 
        if (user.Person is not null)
            throw new CustomException((int)MessagesCodesError.UserHasAssignedPerson, $"El usuario posee asignado a la persona : '{request.DocumentNumber}'");
        ConfigureUserIdAndUserNameInContext(user.Id, user.UserName);
        //Formas de registro
        if (!user.UserRegistrationForms.Any(any => any.UserTypeRegister == UserTypeRegister.Google))
            throw new CustomException((int)MessagesCodesError.UserHasNoRegistrationForm, $"El usuario no tiene una forma de registro: '{UserTypeRegister.Google}'");
        return user;
    }

    /// <summary>
    /// Imple,entaciòn Login
    /// </summary>
    /// <param name="request"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    protected override async Task<LoginResponse> GetLoginAsync(AssignPersonRequest request, User user) => await Mediator.Send(new LoginRequest
    {
        UserName = request.Email,
        Password = request.Password,
        LoginType = request.LoginType,
        ContextRequest = ContextRequest,
    }).ConfigureAwait(false);
}
