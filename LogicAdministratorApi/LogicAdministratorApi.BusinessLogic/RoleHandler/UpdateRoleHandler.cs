using LogicAdministratorApi.Model.Request.Role;
using LogicAdministratorApi.Model.Response.Role;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Models.Enums;

namespace LogicAdministratorApi.BusinessLogic.RoleHandler;

/// <summary>
/// Handler para actualizar un rol
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateRoleHandler(
    ILogger<UpdateRoleHandler> logger,
    IPluginFactory pluginFactory) : RoleBase<UpdateRoleRequest, UpdateRoleResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la actualización de un rol
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<UpdateRoleResponse> Handle(UpdateRoleRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.UpdateRole, request, async () =>
        {
            // Obtener el rol por GUID (sin include porque necesitamos actualizarlo)
            var role = await UnitOfWork.RoleRepository.GetByFirstOrDefaultAsync(
                where => where.Guid == request.RoleGuid
            ).ConfigureAwait(false)
            ?? throw new CustomException((int)MessagesCodesError.SystemError, "Rol no encontrado");

            // Validar que el nombre del rol no exista en otro rol
            if (await UnitOfWork.RoleRepository
                .ExistAnyAsync(where => where.Code.ToLower() == request.Name.ToLower() && where.Guid != request.RoleGuid)
                .ConfigureAwait(false))
                throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un rol con este nombre");

            // Validar que las funcionalidades existen
            if (request.FunctionalityGuids.Any())
            {
                var functionalitiesCount = await UnitOfWork.FunctionalityRepository
                    .CountAsync(where => request.FunctionalityGuids.Contains(where.Id))
                    .ConfigureAwait(false);

                if (functionalitiesCount != request.FunctionalityGuids.Count)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Una o más funcionalidades no fueron encontradas");
            }

            // Actualizar propiedades del rol
            role.Code = request.Name;
            role.Name = request.Description;
            role.Scope = request.ScopeCode.ToEnum<RoleScope>();

            await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);

            // Eliminar funcionalidades existentes del rol
            var existingRoleFunctionalities = await UnitOfWork.RoleFunctionalityRepository
                .GetGenericAsync(select => select, where => where.RoleId == role.Id)
                .ConfigureAwait(false);

            foreach (var existingRoleFunctionality in existingRoleFunctionalities)
            {
                await UnitOfWork.RoleFunctionalityRepository.DeleteAsync(existingRoleFunctionality).ConfigureAwait(false);
            }

            // Asignar nuevas funcionalidades al rol
            if (request.FunctionalityGuids.Any())
            {
                var roleFunctionalities = request.FunctionalityGuids.Select(functionalityId => new RoleFunctionality
                {
                    RoleId = role.Id,
                    FunctionalityId = functionalityId
                }).ToList();

                await UnitOfWork.RoleFunctionalityRepository.AddRangeAsync(roleFunctionalities).ConfigureAwait(false);
            }

            await UnitOfWork.RoleRepository.UpdateAsync(role).ConfigureAwait(false);
            await UnitOfWork.CommitAsync().ConfigureAwait(false);

            return new UpdateRoleResponse
            {
                RoleGuid = role.Guid,
                Name = role.Code,
                UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                ShowMessage = true
            };
        }).ConfigureAwait(false);
}
