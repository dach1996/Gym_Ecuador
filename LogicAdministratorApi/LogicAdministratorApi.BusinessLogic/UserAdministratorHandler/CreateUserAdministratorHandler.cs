using Common.Utils.Cryptography.Argon2;
using LogicAdministratorApi.Model.Request.UserAdministrator;
using LogicAdministratorApi.Model.Response.UserAdministrator;
using PersistenceDb.Models.Authentication;

namespace LogicAdministratorApi.BusinessLogic.UserAdministratorHandler;

/// <summary>
/// Handler para crear usuario administrador
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateUserAdministratorHandler(
    ILogger<CreateUserAdministratorHandler> logger,
    IPluginFactory pluginFactory) : UserAdministratorBase<CreateUserAdministratorRequest, CreateUserAdministratorResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de un usuario administrador
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateUserAdministratorResponse> Handle(CreateUserAdministratorRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.CreateUserAdministrator, request, async () =>
            {
                // Obtener la persona por el número de identificación
                var personDetails = await GetPersonByDocumentNumberAsync(request.IdentificationNumber).ConfigureAwait(false);
                var personId = await UnitOfWork.PersonRepository.GetFirstOrDefaultGenericAsync(select => select.Id, where => where.Guid == personDetails.Guid).ConfigureAwait(false);
                // Validar que el email no exista
                if (await UnitOfWork.UserRepository
                    .ExistAnyAsync(where => where.Email.ToLower() == request.Email.ToLower())
                    .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un usuario con este email");

                // Validar que el username no exista
                if (!string.IsNullOrEmpty(request.UserName) && await UnitOfWork.UserRepository
                    .ExistAnyAsync(where => where.UserName.ToLower() == request.UserName.ToLower())
                    .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un usuario con este nombre de usuario");

                // Generar salt y contraseña
                var salt = Argon2.GenerateRandomSecretBytes();
                var passwordEncrypted = GetPasswordEncrypted(request.Password, salt);
                var roleType = RoleType.Admin.GetEnumMember();
                var platformType = RolePlatformType.Web.GetEnumMember();
                var roleId = (await UnitOfWork.RoleRepository.GetFirstOrDefaultGenericAsync(
                    select => (int?)select.Id,
                     where => where.Name == roleType && where.Platform.Code == platformType).ConfigureAwait(false))
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "No se encontró el rol de administrador");
                var userRoleScopes = await GetScopesAsync().ConfigureAwait(false);
                var scope = userRoleScopes.FirstOrDefault(where => where.Code == RoleScopeType.Gym.GetEnumMember())
                 ?? throw new CustomException((int)MessagesCodesError.SystemError, "No se encontró el scope de negocio");
                var gymId = await UnitOfWork.GymRepository.GetFirstOrDefaultGenericAsync(
                    select => (int?)select.Id,
                     where => where.Guid == request.GymGuid).ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "No se encontró el gimnasio");
                // Crear el nuevo usuario
                var newUser = new User
                {
                    Guid = Guid.NewGuid(),
                    UserName = request.UserName,
                    Email = request.Email,
                    Phone = request.Phone,
                    PersonId = personId,
                    LanguageCode = request.LanguageCode ?? "es",
                    DateTimeRegister = Now,
                    IsBlocked = false,
                    HasCompleteRegistration = false,
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
                            ScopeId = scope.Id,
                            ScopeIdentifier = gymId
                        }
                    ]
                };

                // Guardar en la base de datos
                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                await UnitOfWork.UserRepository.AddAsync(newUser).ConfigureAwait(false);
                await UnitOfWork.CommitAsync().ConfigureAwait(false);

                return new CreateUserAdministratorResponse(newUser.Guid, newUser.UserName, newUser.Email)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);
}

