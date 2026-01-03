using Common.WebCommon.Helper;
using Common.WebCommon.Models.Enum;
using LogicAdministratorApi.Model.Request.UserAdministrator;
using LogicAdministratorApi.Model.Response.UserAdministrator;
using PersistenceDb.Models.Authentication;

namespace LogicAdministratorApi.BusinessLogic.UserAdministratorHandler;

/// <summary>
/// Handler para eliminar usuario administrador
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class DeleteUserAdministratorHandler(
    ILogger<DeleteUserAdministratorHandler> logger,
    IPluginFactory pluginFactory) : UserAdministratorBase<DeleteUserAdministratorRequest, DeleteUserAdministratorResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la eliminación de un usuario administrador
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<DeleteUserAdministratorResponse> Handle(DeleteUserAdministratorRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.DeleteUserAdministrator, request, async () =>
            {
                // Buscar el usuario por GUID
                var user = await UnitOfWork.UserRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.UserGuid)
                    .ConfigureAwait(false);

                if (user == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Usuario no encontrado");

                // Validar que no se esté eliminando a sí mismo
                if (user.Guid == CurrentUserGuid)
                    throw new CustomException((int)MessagesCodesError.SystemError, "No se puede eliminar su propio usuario");

                // Eliminar el usuario
                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                await UnitOfWork.UserRepository.DeleteAsync(user).ConfigureAwait(false);
                await UnitOfWork.CommitAsync().ConfigureAwait(false);

                return new DeleteUserAdministratorResponse(true)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);
}

