using Common.Queue.Model.Template;
using LogicApi.Model.Request.Authorization;
using LogicApi.Model.Response.Authorization;
namespace LogicApi.BusinessLogic.AuthorizationHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class PasswordForgottenHandler(
    ILogger<PasswordForgottenHandler> logger,
    IPluginFactory pluginFactory) : AuthorizationBase<PasswordForgottenRequest, PasswordForgottenResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<PasswordForgottenResponse> Handle(PasswordForgottenRequest request, CancellationToken cancellationToken)
    => await ExecuteHandlerAsync(OperationApiName.PasswordForgotten, request, async () =>
            await AuthenticationUnitOfWork.BeginTransactionStrategyAsync(async () =>
            {
                var userCurrent = (await AuthenticationUnitOfWork.UserRepository.GetFirstOrDefaultGenericAsync(
                               select => new
                               {
                                   select.Id,
                                   select.UserName,
                                   select.Email,
                                   select.HasCompleteRegistration,
                                   select.IsBlocked,
                                   select.Salt,
                               },
                               where => where.Email == request.Email
                           ).ConfigureAwait(false))
                           ?? throw new CustomException((int)MessagesCodesError.InfoUserNotFound, $"No se pudo encontrar el correo: '{request.Email}'");
                if (!userCurrent.HasCompleteRegistration)
                    throw new CustomException((int)MessagesCodesError.UserHasNoRegistrationForm, $"El usuario no ha completado el proceso de registro");
                //Valida que el usuario no esté bloqueado 
                if (userCurrent.IsBlocked)
                    throw new CustomException((int)MessagesCodesError.UserBlocked);
                //Genera Nueva Contraseña
                var newPassword = GeneratePassword();
                //Actualiza información de Contraseña
                var passwordEncrypted = GetPasswordEncrypted(newPassword, userCurrent.Salt);
                //Actualiza el usuario
                await AuthenticationUnitOfWork.UserRegistrationFormRepository.UpdateByAsync(
                     user => new()
                     {
                         PasswordTemporary = passwordEncrypted
                     },
                     where => where.UserId == userCurrent.Id && where.UserTypeRegister == UserTypeRegister.Manual,
                     throwExceptionIfNoRecordsAffected: true
                 ).ConfigureAwait(false);
                await SendQueueMessageAsync(new ForgottenPasswordMailQueueTemplate
                {
                    NewPassword = newPassword,
                    UserName = userCurrent.UserName,
                    To = userCurrent.Email.ToListElements(),
                }).ConfigureAwait(false);
                //Genera la Respuesta
                return new PasswordForgottenResponse
                {
                    ShowMessage = true,
                    TemporalPassword = newPassword,
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.CreateTemporalityPasswordSuccess)
                };
            }).ConfigureAwait(false)
        , UnitOfWorkType.Authentication, true);
}
