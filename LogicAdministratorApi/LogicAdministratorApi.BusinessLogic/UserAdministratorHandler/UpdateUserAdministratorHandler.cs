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
                    where => where.Guid == request.UserGuid,
                    include => include.UserRegistrationForms)
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

            await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            await UnitOfWork.UserRepository.UpdateAsync(user).ConfigureAwait(false);
            await UnitOfWork.CommitAsync().ConfigureAwait(false);

            return new UpdateUserAdministratorResponse(user.Guid, user.UserName, user.Email)
            {
                UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                ShowMessage = true
            };
        }
        ).ConfigureAwait(false);
}

