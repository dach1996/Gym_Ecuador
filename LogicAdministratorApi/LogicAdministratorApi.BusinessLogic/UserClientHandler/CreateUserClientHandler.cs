using Common.Templates.Models.Mail;
using Common.Utils.Cryptography.Argon2;
using LogicAdministratorApi.Model.Request.UserClient;
using LogicAdministratorApi.Model.Response.UserClient;
using LogicCommon.Model.Request.Mail;
using LogicCommon.Model.Request.Person;
using PersistenceDb.Models.Authentication;

namespace LogicAdministratorApi.BusinessLogic.UserClientHandler;

/// <summary>
/// Handler para crear usuario cliente
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateUserClientHandler(
    ILogger<CreateUserClientHandler> logger,
    IPluginFactory pluginFactory) : UserClientBase<CreateUserClientRequest, CreateUserClientResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de un usuario cliente
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateUserClientResponse> Handle(CreateUserClientRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.CreateUserClient, request, async () =>
            {
                // Obtener la persona por el número de identificación
                var personDetails = await Mediator.Send(new GetPersonByDocumentNumberCommonRequest(request.ContextRequest, request.IdentificationNumber), cancellationToken).ConfigureAwait(false);

                // Validar que el email no exista
                if (await UnitOfWork.UserRepository
                    .ExistAnyAsync(where => where.Email.ToLower() == request.Email.ToLower()
                    || where.UserName.ToLower() == request.Email.ToLower())
                    .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un usuario con este email");

                // Generar salt y contraseña
                var salt = Argon2.GenerateRandomSecretBytes();
                var password = GeneratePassword();
                var passwordEncrypted = GetPasswordEncrypted(password, salt);

                // Obtener el rol de cliente móvil
                var roleId = await UnitOfWork.RoleRepository.GetIdByScopeAndPlatformAsync(
                        RoleType.Client, RolePlatformType.Mobile).ConfigureAwait(false);

                // Obtener el scope global
                var scope = (await GetScopesAsync().ConfigureAwait(false))
                    .Find(where => where.Code == RoleScopeType.Global.GetEnumMember())
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "No se encontró el scope global");

                // Crear el nuevo usuario
                var newUser = new User
                {
                    Guid = Guid.NewGuid(),
                    UserName = request.Email,
                    Email = request.Email,
                    Phone = request.Phone,
                    PersonId = personDetails.Person.Id,
                    LanguageCode = request.LanguageCode ?? "es",
                    DateTimeRegister = Now,
                    IsBlocked = false,
                    HasCompleteRegistration = true,
                    Salt = salt,
                    UserRegistrationForms =
                    [
                        new ()
                        {
                            DateTimeRegister = Now,
                            DateTimeLastAccess = Now,
                            UserTypeRegister = UserTypeRegister.Manual,
                            Password = passwordEncrypted,
                            PasswordTemporary = passwordEncrypted
                        }
                    ],
                    UserRoleScopes = [
                        new ()
                        {
                            RoleId = roleId,
                            ScopeId = scope.Id
                        }
                    ]
                };

                // Guardar en la base de datos
                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                await UnitOfWork.UserRepository.AddAsync(newUser).ConfigureAwait(false);
                await UnitOfWork.CommitAsync().ConfigureAwait(false);
                await Mediator.Send(new SendMailRequest
                {
                    MailTemplateModel = new NewUserManualRegisterMailTemplateModel
                    {
                        PersonName = personDetails.Person?.Names.Split(' ').FirstOrDefault() + " " + personDetails.Person.LastNames?.Split(' ').FirstOrDefault(),
                        Email = newUser.UserName,
                        Password = password,
                        SupportEmail = "soporte@fitecenter.fit",
                        Link = "https://fitcenter-administrator-app-service.azurewebsites.net/"
                    },
                    To = [newUser.Email]
                }).ConfigureAwait(false);
                return new CreateUserClientResponse(newUser.Guid, newUser.UserName, newUser.Email)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);
}

