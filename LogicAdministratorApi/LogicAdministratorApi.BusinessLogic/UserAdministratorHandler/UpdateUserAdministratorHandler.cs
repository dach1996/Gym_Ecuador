using Common.Utils.Cryptography.Argon2;
using Common.WebCommon.Helper;
using Common.WebCommon.Models.Enum;
using LogicAdministratorApi.Model.Request.UserAdministrator;
using LogicAdministratorApi.Model.Response.UserAdministrator;
using PersistenceDb.Models.Authentication;

namespace LogicAdministratorApi.BusinessLogic.UserAdministratorHandler;

/// <summary>
/// Handler para actualizar usuario administrador
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateUserAdministratorHandler(
    ILogger<UpdateUserAdministratorHandler> logger,
    IPluginFactory pluginFactory) : UserAdministratorBase<UpdateUserAdministratorRequest, UpdateUserAdministratorResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la actualización de un usuario administrador
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<UpdateUserAdministratorResponse> Handle(UpdateUserAdministratorRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.UpdateUserAdministrator, request, async () =>
        {
            // Buscar el usuario por GUID
            var user = await UnitOfWork.UserRepository
                .GetByFirstOrDefaultAsync(
                    where => where.Guid == request.UserGuid)
                .ConfigureAwait(false)
            ?? throw new CustomException((int)MessagesCodesError.SystemError, "Usuario no encontrado");

            // Validar que el email no exista en otro usuario
            if (!string.IsNullOrEmpty(request.Email) && await UnitOfWork.UserRepository
                .ExistAnyAsync(where => where.Email.ToLower() == request.Email.ToLower() && where.Id != user.Id)
                .ConfigureAwait(false))
                throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un usuario con este email");

            // Validar que el username no exista en otro usuario
            if (!string.IsNullOrEmpty(request.UserName) && await UnitOfWork.UserRepository
                .ExistAnyAsync(where => where.UserName.ToLower() == request.UserName.ToLower() && where.Id != user.Id)
                .ConfigureAwait(false))
                throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un usuario con este nombre de usuario");

            // Actualizar los campos
            if (!string.IsNullOrEmpty(request.UserName))
                user.UserName = request.UserName;

            if (!string.IsNullOrEmpty(request.Email))
                user.Email = request.Email;

            if (!string.IsNullOrEmpty(request.Phone))
                user.Phone = request.Phone;

            if (!string.IsNullOrEmpty(request.LanguageCode))
                user.LanguageCode = request.LanguageCode;

            if (request.IsBlocked.HasValue)
                user.IsBlocked = request.IsBlocked.Value;

            // Actualizar contraseña si se proporciona
            if (!string.IsNullOrEmpty(request.Password))
            {
                var passwordEncrypted = GetPasswordEncrypted(request.Password, user.Salt);
                if (user.ManualUserRegistrationForm != null)
                {
                    user.ManualUserRegistrationForm.Password = passwordEncrypted;
                    user.ManualUserRegistrationForm.PasswordTemporary = passwordEncrypted;
                }
            }

            // Actualizar roles si se proporcionan
            if (request.RoleGuids != null && request.RoleGuids.Any())
            {
                // Obtener el gimnasio si se proporciona
                int? gymId = null;
                if (request.GymGuid.HasValue && request.GymGuid.Value != Guid.Empty)
                {
                    gymId = await UnitOfWork.GymRepository.GetFirstOrDefaultGenericAsync(
                        select => (int?)select.Id,
                        where => where.Guid == request.GymGuid.Value).ConfigureAwait(false)
                        ?? throw new CustomException((int)MessagesCodesError.SystemError, "No se encontró el gimnasio");
                }

                // Validar y obtener los roles por GUID
                var roles = await UnitOfWork.RoleRepository.GetGenericAsync(
                    select => new { select.Id, select.Guid, select.Scope },
                    where => request.RoleGuids.Contains(where.Guid)
                ).ConfigureAwait(false);

                if (roles.Count() != request.RoleGuids.Count)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Uno o más roles no fueron encontrados");

                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);

                // Eliminar roles existentes
                var existingUserRoleScopes = await UnitOfWork.UserRoleScopeRepository
                    .GetGenericAsync(select => select, where => where.UserId == user.Id)
                    .ConfigureAwait(false);

                foreach (var existingRoleScope in existingUserRoleScopes)
                {
                    await UnitOfWork.UserRoleScopeRepository.DeleteAsync(existingRoleScope).ConfigureAwait(false);
                }

                // Agregar nuevos roles
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

                    var newRoleScope = new UserRoleScope
                    {
                        UserId = user.Id,
                        RoleId = role.Id,
                        ScopeIdentifier = scopeIdentifier
                    };

                    await UnitOfWork.UserRoleScopeRepository.AddAsync(newRoleScope).ConfigureAwait(false);
                }

                await UnitOfWork.UserRepository.UpdateAsync(user).ConfigureAwait(false);
                await UnitOfWork.CommitAsync().ConfigureAwait(false);
            }
            else
            {
                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                await UnitOfWork.UserRepository.UpdateAsync(user).ConfigureAwait(false);
                await UnitOfWork.CommitAsync().ConfigureAwait(false);
            }

            return new UpdateUserAdministratorResponse(user.Guid, user.UserName, user.Email)
            {
                UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                ShowMessage = true
            };
        }
        ).ConfigureAwait(false);
}

