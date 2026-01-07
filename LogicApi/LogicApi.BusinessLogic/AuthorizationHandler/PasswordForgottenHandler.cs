using Common.Queue.Model.Template;
using Common.Templates.Models.Mail;
using LogicApi.Model.Request.Authorization;
using LogicApi.Model.Response.Authorization;
using LogicCommon.Model.Request.Mail;
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
            await UnitOfWork.BeginTransactionStrategyAsync(async () =>
            {
                var userCurrent = (await UnitOfWork.UserRepository.GetFirstOrDefaultGenericAsync(
                               select => new
                               {
                                   select.Id,
                                   select.UserName,
                                   select.Email,
                                   select.HasCompleteRegistration,
                                   select.IsBlocked,
                                   select.Salt,
                                   select.Person.RealNames,
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
                await UnitOfWork.UserRegistrationFormRepository.UpdateByAsync(
                     (user => user.PasswordTemporary, passwordEncrypted),
                     where => where.UserId == userCurrent.Id && where.UserTypeRegister == UserTypeRegister.Manual
                 ).ConfigureAwait(false);

                await Mediator.Send(new SendMailRequest
                {
                    MailTemplateModel = new ForgottenPasswordMailMailTemplateModel
                    {
                        Link = "https://fitcenter-app-service.azurewebsites.net/",
                        PolityLink = "https://fitcenter.fit/politica-de-privacidad",
                        Password = newPassword,
                        FirstName = userCurrent.RealNames.Split(' ').FirstOrDefault(),
                    },
                    To = [userCurrent.Email],
                }).ConfigureAwait(false);
                //Genera la Respuesta
                return new PasswordForgottenResponse
                {
                    ShowMessage = true,
                    TemporalPassword = newPassword,
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.CreateTemporalityPasswordSuccess)
                };
            }).ConfigureAwait(false)
        , true);
}
