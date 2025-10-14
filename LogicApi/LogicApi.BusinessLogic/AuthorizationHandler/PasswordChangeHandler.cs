using Common.Utils.Cryptography.Argon2;
using LogicApi.Model.Request.Authorization;
namespace LogicApi.BusinessLogic.AuthorizationHandler;
public class PasswordChangeHandler(
    ILogger<PasswordChangeHandler> logger,
    IPluginFactory pluginFactory) : AuthorizationBase<PasswordChangeRequest, HandlerResponse>(
        logger,
        pluginFactory)
{


    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override async Task<HandlerResponse> Handle(PasswordChangeRequest request, CancellationToken cancellationToken)
    => await ExecuteHandlerAsync(OperationApiName.PasswordChange, request, async () =>
        {
            //Valida que las contraseñas no sean iguales
            if (request.NewPassword.Equals(request.CurrentPassword))
                throw new CustomException((int)MessagesCodesError.EqualsPassword);
            //Obtiene el usuario actual
            var currentUser = (await UnitOfWork.UserRepository.GetFirstOrDefaultGenericAsync(
                select => new
                {
                    select.Salt,
                    UserRegistrationForms = select.UserRegistrationForms.Where(where => where.UserTypeRegister == UserTypeRegister.Manual)
                    .Select(select => new
                    {
                        select.Password,
                        select.PasswordTemporary
                    })
                    .FirstOrDefault()
                },
                where => where.Id == UserId
            ).ConfigureAwait(false))
                ?? throw new CustomException((int)MessagesCodesError.InfoUserNotFound, $"El usuario con id {UserId} no existe");
            //Verifica si existe una contraseña válida
            if (!Argon2.VerifyHashes(request.CurrentPassword, [currentUser.UserRegistrationForms.Password, currentUser.UserRegistrationForms.PasswordTemporary], currentUser.Salt))
                throw new CustomException((int)MessagesCodesError.IncorrectPassword);
            await UnitOfWork.UserRegistrationFormRepository.UpdateByAsync(
                user => new()
                {
                    Password = GetPasswordEncrypted(request.NewPassword, currentUser.Salt),
                    PasswordTemporary = null
                },
                where => where.UserId == UserId && where.UserTypeRegister == UserTypeRegister.Manual,
                throwExceptionIfNoRecordsAffected: true
            ).ConfigureAwait(false);
            return HandlerResponse.Complete(GetSuccessMessage(MessagesCodesSucess.ChangePasswordSuccess), true);
        });
}
