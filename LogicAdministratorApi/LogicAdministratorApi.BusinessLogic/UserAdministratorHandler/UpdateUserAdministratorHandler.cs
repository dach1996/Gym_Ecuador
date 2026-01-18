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

            // Actualizar los campos
            if (!string.IsNullOrEmpty(request.Phone))
                user.Phone = request.Phone;

            if (request.IsBlocked.HasValue)
                user.IsBlocked = request.IsBlocked.Value;
            UserRegistrationForm manualUserRegistrationForm = null;
            // Actualizar contraseña si se proporciona
            if (!string.IsNullOrEmpty(request.Password))
            {
                var passwordEncrypted = GetPasswordEncrypted(request.Password, user.Salt);
                manualUserRegistrationForm = await UnitOfWork.UserRegistrationFormRepository.GetByFirstOrDefaultAsync(
                    where => where.UserId == user.Id && where.UserTypeRegister == UserTypeRegister.Manual
                ).ConfigureAwait(false)
                ?? throw new CustomException((int)MessagesCodesError.SystemError, "Formulario de registro manual no encontrado");
                manualUserRegistrationForm.Password = passwordEncrypted;
                manualUserRegistrationForm.PasswordTemporary = passwordEncrypted;
            }
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

            await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            await UnitOfWork.UserRoleScopeRepository.DeleteAsync(where => where.UserId == user.Id).ConfigureAwait(false);
            if (manualUserRegistrationForm != null)
                await UnitOfWork.UserRegistrationFormRepository.UpdateAsync(manualUserRegistrationForm).ConfigureAwait(false);
            await UnitOfWork.UserRepository.UpdateAsync(user).ConfigureAwait(false);
            await UnitOfWork.UserRoleScopeRepository.AddAsync(new UserRoleScope
            {
                UserId = user.Id,
                RoleId = roles.Id,
                ScopeIdentifier = identifier
            }).ConfigureAwait(false);
            await UnitOfWork.CommitAsync().ConfigureAwait(false);
            return new UpdateUserAdministratorResponse(user.Guid, user.UserName, user.Email)
            {
                UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                ShowMessage = true
            };
        }
        ).ConfigureAwait(false);
}

