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
                if (!string.IsNullOrEmpty(request.UserName) && await UnitOfWork.UserRepository
                    .ExistAnyAsync(where => where.UserName.ToLower() == request.UserName.ToLower())
                    .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un usuario con este nombre de usuario");

                // Generar salt y contraseña
                var salt = Argon2.GenerateRandomSecretBytes();
                var passwordEncrypted = GetPasswordEncrypted(request.Password, salt);
                
                // Obtener el gimnasio si se proporciona
                int? gymId = null;
                if (request.GymGuid != Guid.Empty)
                {
                    gymId = await UnitOfWork.GymRepository.GetFirstOrDefaultGenericAsync(
                        select => (int?)select.Id,
                        where => where.Guid == request.GymGuid).ConfigureAwait(false)
                        ?? throw new CustomException((int)MessagesCodesError.SystemError, "No se encontró el gimnasio");
                }

                // Validar y obtener los roles por GUID
                var userRoleScopes = new List<UserRoleScope>();
                if (request.RoleGuids != null && request.RoleGuids.Any())
                {
                    var roles = await UnitOfWork.RoleRepository.GetGenericAsync(
                        select => new { select.Id, select.Guid, select.Scope },
                        where => request.RoleGuids.Contains(where.Guid)
                    ).ConfigureAwait(false);

                    if (roles.Count != request.RoleGuids.Count)
                        throw new CustomException((int)MessagesCodesError.SystemError, "Uno o más roles no fueron encontrados");

                    foreach (var role in roles)
                    {
                        int? scopeIdentifier = null;
                        
                        // Si el rol requiere alcance Gym o GymBranch, se necesita el gymId
                        if (role.Scope == RoleScope.Gym || role.Scope == RoleScope.GymBranch)
                        {
                            if (!gymId.HasValue)
                                throw new CustomException((int)MessagesCodesError.SystemError, $"El rol {role.Guid} requiere un gimnasio");
                            scopeIdentifier = gymId.Value;
                        }

                        userRoleScopes.Add(new UserRoleScope
                        {
                            RoleId = role.Id,
                            ScopeIdentifier = scopeIdentifier
                        });
                    }
                }
                else
                {
                    // Si no se proporcionan roles, usar el comportamiento por defecto
                    var roleType = RoleScope.Gym.GetEnumMember();
                    var platformType = RolePlatformType.Web.GetEnumMember();
                    var roleIdNullable = await UnitOfWork.RoleRepository.GetFirstOrDefaultGenericAsync(
                        select => (int?)select.Id,
                        where => where.Code == roleType && where.Platform.Code == platformType).ConfigureAwait(false);
                    
                    if (!roleIdNullable.HasValue)
                        throw new CustomException((int)MessagesCodesError.SystemError, "No se encontró el rol de administrador");
                    
                    if (!gymId.HasValue)
                        throw new CustomException((int)MessagesCodesError.SystemError, "No se encontró el gimnasio");

                    userRoleScopes.Add(new UserRoleScope
                    {
                        RoleId = roleIdNullable.Value,
                        ScopeIdentifier = gymId
                    });
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
                    UserRoleScopes = userRoleScopes
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

