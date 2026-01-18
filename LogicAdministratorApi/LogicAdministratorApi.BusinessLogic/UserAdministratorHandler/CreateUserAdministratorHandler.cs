using Common.Utils.Cryptography.Argon2;
using LogicAdministratorApi.Model.Request.UserAdministrator;
using LogicAdministratorApi.Model.Response.UserAdministrator;
using LogicCommon.Model.Request.Person;
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
                var personDetails = await Mediator.Send(new GetPersonByDocumentNumberCommonRequest(request.ContextRequest, request.IdentificationNumber), cancellationToken).ConfigureAwait(false);
                var personId = personDetails.Person.Id;
                // Validar que el email no exista
                if (await UnitOfWork.UserRepository
                    .ExistAnyAsync(where => where.Email.ToLower() == request.Email.ToLower())
                    .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un usuario con este email");

                // Validar que el username no exista
                if (await UnitOfWork.UserRepository
                    .ExistAnyAsync(where => where.UserName.ToLower() == request.UserName.ToLower())
                    .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un usuario con este nombre de usuario");

                // Generar salt y contraseña
                var salt = Argon2.GenerateRandomSecretBytes();
                var passwordEncrypted = GetPasswordEncrypted(request.Password, salt);

                var roles = (await GetRolesAsync().ConfigureAwait(false)).Find(where => where.Guid == request.RoleGuid)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Rol no encontrado");
                // Si el rol requiere un gimnasio o sucursal, se necesita el identifier
                if (new[] { RoleScope.Gym, RoleScope.GymBranch }.Contains(roles.Scope) && !request.Identifier.HasValue)
                    throw new CustomException((int)MessagesCodesError.SystemError, "El rol requiere un identificador de asignación");
                var identifier = (int?)null;
                // Validar que el gimnasio existe
                if (RoleScope.Gym == roles.Scope)
                {
                    identifier = await UnitOfWork.GymRepository.GetFirstOrDefaultGenericAsync(
                        select => (int?)select.Id,
                        where => where.Guid == request.Identifier.Value).ConfigureAwait(false);
                }
                // Validar que la sucursal de gimnasio existe
                if (RoleScope.GymBranch == roles.Scope)
                {
                    identifier = await UnitOfWork.GymBranchRepository.GetFirstOrDefaultGenericAsync(
                        select => (int?)select.Id,
                        where => where.Guid == request.Identifier.Value).ConfigureAwait(false);
                }
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
                    UserRoleScopes = [.. new List<UserRoleScope>
                    {
                        new ()
                        {
                            RoleId = roles.Id,
                            ScopeIdentifier = identifier
                        }
                    }]
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

